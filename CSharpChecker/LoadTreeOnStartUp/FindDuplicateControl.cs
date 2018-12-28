using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project = Microsoft.Build.Evaluation.Project;
using ProjectItem = Microsoft.Build.Evaluation.ProjectItem;
using BuildArchitecture;
using EnvDTE;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Window = EnvDTE.Window;
using System.IO;

namespace CSharpChecker.LoadTreeOnStartUp
{
    public partial class FindDuplicateControl : UserControl
    {
        private IVsSolution _solService;
        public WorkSpace WorkSpace;
        private DTE _dTE;
        private List<string> _filePaths;
        /// <summary>
        /// Initializes a new instance of the <see cref="FindDuplicateControl"/> class.
        /// </summary>
        public FindDuplicateControl()
        {
            this.InitializeComponent();
            WorkSpace = WorkSpace.Instance;
            _solService = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
            _dTE = Package.GetGlobalService(typeof(DTE)) as DTE;
            GetListFilePath();
            TestButton();
        }

        private void GetListFilePath()
        {
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

        public void TestButton()
        {
            List<ErrorInformation> errorInformation = new List<ErrorInformation> { new ErrorInformation { DisplayText = "asd",ErrorCode = "dasfw" } };
            dataGridView1.DataSource = errorInformation;
            OpenSourceFile(_filePaths[0]);
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
                textSelection.MoveToDisplayColumn(20, 0);
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
