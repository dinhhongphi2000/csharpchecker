using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using Task = System.Threading.Tasks.Task;

namespace CSharpChecker.SolutionLoad
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(SolutionLoad.PackageGuidString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionOpening_string, PackageAutoLoadFlags.BackgroundLoad)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class SolutionLoad : AsyncPackage
    {
        /// <summary>
        /// SolutionLoad GUID string.
        /// </summary>
        public const string PackageGuidString = "94970ec4-8233-41d7-a301-ad087210c0c8";
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionLoad"/> class.
        /// </summary>
        public SolutionLoad()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }
        IVsSolution solService;

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // Since this package might not be initialized until after a solution has finished loading,
            // we need to check if a solution has already been loaded and then handle it.
            bool isSolutionLoaded = await IsSolutionLoadedAsync();

            if (isSolutionLoaded)
            {
                HandleOpenSolution();
            }

            // Listen for subsequent solution events
            SolutionEvents.OnAfterOpenSolution += HandleOpenSolution;
        }

        private async Task<bool> IsSolutionLoadedAsync()
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync();
            solService = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;

            ErrorHandler.ThrowOnFailure(solService.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));
            //solService.GetProjectFilesInSolution(1, projectCount, projectNames.ToArray(), out numProjects);
            return (bool)value;
        }

        private void HandleOpenSolution(object sender = null, EventArgs e = null)
        {
            string solutionDirectory;
            string[] projectPaths;
            uint numProjects;
            string solutionFullFileName;
            int projectCount;
            solutionDirectory = GetPropertyValue<string>(solService, __VSPROPID.VSPROPID_SolutionDirectory);
            Debug.WriteLine("Solution directory: " + solutionDirectory);

            solutionFullFileName = GetPropertyValue<string>(solService, __VSPROPID.VSPROPID_SolutionFileName);
            Debug.WriteLine("Solution full file name: " + solutionFullFileName);

            projectCount = GetPropertyValue<int>(solService, __VSPROPID.VSPROPID_ProjectCount);
            Debug.WriteLine("Project count: " + projectCount.ToString());
            projectPaths = new string[projectCount];
            var hr = solService.GetProjectFilesInSolution((uint)__VSGETPROJFILESFLAGS.GPFF_SKIPUNLOADEDPROJECTS, (uint)projectCount, projectPaths, out numProjects);
            Debug.Assert(hr == VSConstants.S_OK, "GetProjectFilesInSolution failed.");

            SolutionInfo solutionInfo = new SolutionInfo(Path.GetFileNameWithoutExtension(solutionFullFileName), solutionFullFileName);
            solutionInfo.ProjectInfomation = GetInfoFromSolution(projectPaths);
            
            Debug.WriteLine("The current time: " + DateTime.Now.ToLongTimeString());
            // Handle the open solution and try to do as much work
            // on a background thread as possible
        }
        private T GetPropertyValue<T>(IVsSolution solutionInterface, __VSPROPID solutionProperty)
        {
            object value = null;
            T result = default(T);
            if (solutionInterface.GetProperty((int)solutionProperty, out value) == VSConstants.S_OK)
            {
                result = (T)value;
            }
            return result;
        }
        private List<ProjectInfo> GetInfoFromSolution (string[] projectPaths)
        {
            List<ProjectInfo> projectInfos = new List<ProjectInfo>();
            foreach (string path in projectPaths)
            {
                Project[] projects = new Project[ProjectCollection.GlobalProjectCollection.GetLoadedProjects(path).Count];
                ProjectCollection.GlobalProjectCollection.GetLoadedProjects(path).CopyTo(projects, 0);
                foreach (Project pro in projects)
                {
                    ProjectInfo currentPI = new ProjectInfo(Path.GetFileNameWithoutExtension(pro.FullPath), pro.FullPath);
                    List<FileInfo> fileInfos = new List<FileInfo>();
                    ProjectItem[] projectItem = new ProjectItem[pro.GetItems("Compile").Count];
                    pro.GetItems("Compile").CopyTo(projectItem, 0);

                    foreach (ProjectItem item in projectItem)
                    {
                        if (!item.EvaluatedInclude.StartsWith("Properties\\"))
                        {
                            var itempath = pro.DirectoryPath + "\\" + item.EvaluatedInclude;
                            currentPI.FileInfos.Add(new FileInfo(Path.GetFileNameWithoutExtension(itempath), itempath));
                        }

                    }
                    projectInfos.Add(currentPI);
                }
                
            }
            return projectInfos;
        }
       

    }
}
