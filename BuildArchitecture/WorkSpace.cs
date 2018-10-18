using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BuildArchitecture.Context;
using System;
using System.Collections.Generic;

namespace BuildArchitecture
{
    public class WorkSpace : IWorkSpace
    {
        //Listener is called when walker visit node. Responsibility for listener is loading all rule in folder or assembly
        private IParseTreeListener _listener = null;
        //Solution info
        private SolutionContext _solutionTree;
        //Walker which visits node of Parser tree and call action of listener
        private ParseTreeWalker _treeWalker = null;
        //Error list
        private List<ErrorInformation> _errorList = new List<ErrorInformation>();
        public ProjectContext CurrentProject { get; set; }
        public string CurrentFile { get; set; }

        public ParseTreeWalker TreeWalker
        {
            get { return _treeWalker; }
            private set { }
        }

        public WorkSpace(SolutionContext root)
        {
            _solutionTree = root ?? throw new ArgumentNullException("SolutionContext can't null");
            CurrentProject = root.GetProject(0);
            //Initial default value
            _listener = new NodeVisitedListener(_errorList);
            _treeWalker = new ParseTreeWalker();
        }

        public void UpdateTree(string content = null)
        {
            if (content != null && CurrentFile != null)
            {
                //Build Parser tree from content and save it
                AntlrInputStream stream = new AntlrInputStream(content);
                CSharpLexer lexer = new CSharpLexer(stream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CSharpParser parser = new CSharpParser(tokens);
                CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
                _solutionTree.UpdateProject(CurrentProject.Name, CurrentFile, startContext);
            }
        }

        public List<ErrorInformation> RunRules()
        {
            _errorList.Clear();
            //Walker tree to check rule and add error to error list
            _treeWalker.Walk(_listener, CurrentProject.GetRuleContextOfFile(CurrentFile));
            return _errorList;
        }

        public void SetListener(IParseTreeListener listener)
        {
            _listener = listener ?? throw new ArgumentNullException("Listener must have value");
        }
    }
}
