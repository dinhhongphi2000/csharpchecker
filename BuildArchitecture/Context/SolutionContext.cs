using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildArchitecture.Context
{
    public class SolutionContext
    {
        public string SourcePath { get; set; }
        private Dictionary<string, ProjectContext> _projects;

        public string Name { get; set; }

        public SolutionContext(string solutionPath, string solutionName)
        {
            _projects = new Dictionary<string, ProjectContext>();
            SourcePath = solutionPath;
            Name = solutionName;
        }

        public List<ProjectContext> GetProjects()
        {
            return _projects.Values.ToList();
        }

        public ProjectContext GetProject(string projectName)
        {
            return _projects[projectName];
        }

        public ProjectContext GetProject(int index)
        {
            return _projects.Values.ToList()[0];
        }

        public void AddProjectNode(string projectName, ProjectContext fileContext)
        {
            _projects[projectName] = fileContext ?? throw new ArgumentNullException("Project context is null");
        }

        public void UpdateProject(string projectName, string filePath, ParserRuleContextWithScope context)
        {
            _projects[projectName].UpdateParserRuleOfFile(filePath, context);
        }
    }

}
