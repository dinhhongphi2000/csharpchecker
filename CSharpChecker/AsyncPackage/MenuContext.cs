using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BuildArchitecture;
using EnvDTE;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Project = Microsoft.Build.Evaluation.Project;
using ProjectItem = Microsoft.Build.Evaluation.ProjectItem;
using SolutionEvents = Microsoft.VisualStudio.Shell.Events.SolutionEvents;
using Task = System.Threading.Tasks.Task;
namespace CSharpChecker.LoadTreeOnStartUp
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class MenuContext
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1fb4ab97-3b77-4056-80a7-18ae54e3aab7");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Microsoft.VisualStudio.Shell.AsyncPackage package;
        private IVsSolution _solService;
        private WorkSpace _workSpace;
        private DTE _dTE;
        private List<string> _filePaths;
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuContext"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private MenuContext(Microsoft.VisualStudio.Shell.AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
            _workSpace = WorkSpace.Instance;
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MenuContext Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(Microsoft.VisualStudio.Shell.AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in MenuContext's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new MenuContext(package, commandService);

        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            HandleCheckDuplicate();
            string message = this._workSpace.FindDuplicateFunction();
            string title = "Message";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private async void HandleCheckDuplicate()
        {
            _solService = await package.GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            _dTE = await package.GetServiceAsync(typeof(DTE)) as DTE;

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
                _workSpace.InitOrUpdateParserTreeOfFile(path, GetFileContent(path));
            }
            _workSpace.RunRulesAllFile();
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
    }
}
