using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildArchitecture.Context
{
    public class ProjectContext
    {
        public string SourcePath { get; set; }
        private Dictionary<string, ParserRuleContextWithScope> _parserRuleNodes;

        public string Name { get; set; }

        public ProjectContext(string projectPath, string projectName)
        {
            _parserRuleNodes = new Dictionary<string, ParserRuleContextWithScope>();
            SourcePath = projectPath;
            Name = projectName;
        }

        public List<ParserRuleContextWithScope> GetProjects()
        {
            return _parserRuleNodes.Values.ToList();
        }

        public ParserRuleContextWithScope GetRuleContextOfFile(string filePath)
        {
            return _parserRuleNodes[filePath];
        }

        public void AddParserTreeNode(string filePath, ParserRuleContextWithScope fileContext)
        {
            _parserRuleNodes[filePath] = fileContext ?? throw new ArgumentNullException("ParserRuleContext value is null");
        }

        public void UpdateParserRuleOfFile(string filePath, ParserRuleContextWithScope context)
        {
            _parserRuleNodes[filePath] = context;
        }
    }
}
