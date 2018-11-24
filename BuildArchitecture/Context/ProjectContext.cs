using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildArchitecture.Context
{
    public class ProjectContext
    {
        public string SourcePath { get; set; }
        private Dictionary<string, ParserRuleContext> _parserRuleNodes;

        public string Name { get; set; }

        public ProjectContext(string projectPath, string projectName)
        {
            _parserRuleNodes = new Dictionary<string, ParserRuleContext>();
            SourcePath = projectPath;
            Name = projectName;
        }

        public List<ParserRuleContext> GetProjects()
        {
            return _parserRuleNodes.Values.ToList();
        }

        public ParserRuleContext GetRuleContextOfFile(string filePath)
        {
            return _parserRuleNodes[filePath];
        }

        public void AddParserTreeNode(string filePath, ParserRuleContext fileContext)
        {
            _parserRuleNodes[filePath] = fileContext ?? throw new ArgumentNullException("ParserRuleContext value is null");
        }

        public void UpdateParserRuleOfFile(string filePath, ParserRuleContext context)
        {
            _parserRuleNodes[filePath] = context;
        }
    }
}
