using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BuildArchitecture;
using EnvDTE;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using Project = Microsoft.Build.Evaluation.Project;
using ProjectItem = Microsoft.Build.Evaluation.ProjectItem;
using SolutionEvents = Microsoft.VisualStudio.Shell.Events.SolutionEvents;
using Task = System.Threading.Tasks.Task;

namespace CSharpChecker.LoadTreeOnStartUp
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
    [Guid(LoadTreeOnStartUp.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class LoadTreeOnStartUp : AsyncPackage
    {
        /// <summary>
        /// VSPackage1 GUID string.
        /// </summary>
        public const string PackageGuidString = "38639e3d-226b-4544-a4d6-eea6cb71ac0b";
        private IVsSolution _solService;
        public WorkSpace WorkSpace;
        private DTE _dTE;
        private List<string> _filePaths;
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadTreeOnStartUp"/> class.
        /// </summary>
        public LoadTreeOnStartUp()
        {
            WorkSpace = WorkSpace.Instance;
            SolutionEvents.OnAfterBackgroundSolutionLoadComplete += HandleOpenSolution;
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            bool isSolutionLoad = await IsSolutionLoadedAsync(cancellationToken);
            if (isSolutionLoad) HandleOpenSolution();
            await CSharpChecker.LoadTreeOnStartUp.DuplicateFuncToolCommand.InitializeAsync(this);

            

        }
        private async Task<bool> IsSolutionLoadedAsync(CancellationToken cancellationToken)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            _solService = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            _dTE = await GetServiceAsync(typeof(DTE)) as DTE;
            ErrorHandler.ThrowOnFailure(_solService.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));


            //solService.GetProjectFilesInSolution(1, projectCount, projectNames.ToArray(), out numProjects);
            return value is bool;
        }
        private void HandleOpenSolution(object sender = null, EventArgs e = null)
        {
            IVsActivityLog log = GetService(typeof(SVsActivityLog)) as IVsActivityLog;
            

            
            List<string> projectPaths = new List<string>();
            var items = _dTE.Solution.Projects;
            foreach (var item in items)
            {
                var project = item as EnvDTE.Project;
                projectPaths.Add(project.FullName);
            }

            GetFilePathFromProject(projectPaths, out _filePaths);
            foreach (var path in _filePaths)
            {
                WorkSpace.InitOrUpdateParserTreeOfFile(path, GetFileContent(path));
            }
            WorkSpace.RunRulesAllFile();
            var errors = WorkSpace.GetErrors();
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
        private void GetFilePathFromProject(List<string> projectPaths, out List<string> filePaths)
        {
            filePaths = new List<string>();
            foreach (string path in projectPaths)
            {
                if (path != null)
                {
                    Project[] projects = new Project[ProjectCollection.GlobalProjectCollection.GetLoadedProjects(path).Count];
                    ProjectCollection.GlobalProjectCollection.GetLoadedProjects(path).CopyTo(projects, 0);
                    foreach (Project pro in projects)
                    {
                        ProjectItem[] projectItem = new ProjectItem[pro.GetItems("Compile").Count];
                        pro.GetItems("Compile").CopyTo(projectItem, 0);

                        foreach (ProjectItem item in projectItem)
                        {
                            if (!item.EvaluatedInclude.StartsWith("Properties\\"))
                            {
                                var itempath = pro.DirectoryPath + "\\" + item.EvaluatedInclude;
                                filePaths.Add(itempath);
                            }

                        }
                    }
                }

            }

        }

        public void TestButton()
        {
            OpenSourceFile(_filePaths[0]);
        }

        public void OpenSourceFile(string path)
        {
            EnvDTE.ProjectItem file = _dTE.Solution.FindProjectItem(path);
            var fileName = Path.GetFileName(path);
            Window window;
            TextDocument txtDoc;
            TextSelection textSelection;
            if (file.IsOpen[EnvDTE.Constants.vsViewKindCode])
            {
                window = _dTE.Windows.Item(fileName);
                window.Visible = true;
                txtDoc = window.Document.Object() as TextDocument;
                textSelection = txtDoc.Selection;
                textSelection.MoveToLineAndOffset(20, 0);
            }
            else
            {
                window = file.Open(EnvDTE.Constants.vsViewKindCode);
                window.Visible = true;
                txtDoc = window.Document.Object() as TextDocument;
                textSelection = txtDoc.Selection;
                textSelection.MoveToDisplayColumn(20, 0);
            }
        }

        public string GetFileContent(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion
    }
}
