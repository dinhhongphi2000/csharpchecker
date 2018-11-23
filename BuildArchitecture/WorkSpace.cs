using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace BuildArchitecture
{
    public class WorkSpace
    {
        //Listener is called when walker visit node. Responsibility for listener is loading all rule in folder or assembly
        private NodeVisitedListener _scanTree = null;
        //Walker which visits node of Parser tree and call action of listener
        private ParseTreeWalker _treeWalker = null;
#if TEST
        public Dictionary<string, ParserRuleContextWithScope> _parserRuleContextOfFile;
#else
        private Dictionary<string, ParserRuleContextWithScope> _parserRuleContextOfFile;
#endif
        public ParseTreeWalker TreeWalker
        {
            get { return _treeWalker; }
            private set { }
        }

        public WorkSpace()
        {
            //Initial default value
            _parserRuleContextOfFile = new Dictionary<string, ParserRuleContextWithScope>();
            _scanTree = new NodeVisitedListener();
            _treeWalker = new ParseTreeWalker();
        }

        public void InitOrUpdateParserTreeOfFile(string filePath, string content)
        {
            //Build Parser tree from content and save it
            AntlrInputStream stream = new AntlrInputStream(content);
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);
            CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
            _parserRuleContextOfFile[filePath] = startContext;
        }

        public void RunSemeticAnalysis(string filePath)
        {
            Semetic.V2.SemeticAnalysis analysis = new Semetic.V2.SemeticAnalysis();
            _treeWalker.Walk(analysis, _parserRuleContextOfFile[filePath]);
        }


        public void RunRules(string filePath)
        {
            ParserRuleContextWithScope tree = _parserRuleContextOfFile[filePath];
            _scanTree.ErrorTable.Clear();
            //Walker tree to check rule and add error to error list
            _treeWalker.Walk(_scanTree, tree);
        }

        /// <summary>
        /// Get error list after run rule
        /// </summary>
        /// <returns></returns>
        public List<ErrorInformation> GetErrors()
        {
            return _scanTree.ErrorTable;
        }
    }
}
