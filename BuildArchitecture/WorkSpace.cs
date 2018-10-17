using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;

namespace BuildArchitecture
{
    public class WorkSpace
    {
        //Listener is called when walker visit node. Responsibility for listener is loading all rule in folder or assembly
        private IParseTreeListener _listener = null;
        //Save Parser Tree of file. Trees are separated by Path of file
        Dictionary<string, IParseTree> _fileTree = new Dictionary<string, IParseTree>();
        //Walker which visits node of Parser tree and call action of listener
        private ParseTreeWalker _treeWalker = null;
        public ParseTreeWalker TreeWalker
        {
            get { return _treeWalker; }
            private set { }
        }

        public WorkSpace()
        {
            //Initial default value
            _listener = new NodeVisitedListener();
            _treeWalker = new ParseTreeWalker();
        }

        /// <summary>
        /// Update Syntax Tree of chaged file
        /// </summary>
        /// <param name="filePath">Path of file</param>
        /// <param name="content">Content of file</param>
        public void UpdateTree(string filePath, string content = null)
        {
            if (content != null)
            {
                //Build Parser tree from content and save it
                AntlrInputStream stream = new AntlrInputStream(content);
                CSharpLexer lexer = new CSharpLexer(stream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CSharpParser parser = new CSharpParser(tokens);
                CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
                _fileTree[filePath] = startContext;
            }
        }

        /// <summary>
        /// Check code by running all rule on changed file
        /// </summary>
        /// <param name="filePathChanged">Path of file is changed</param>
        public void RunRules(string filePathChanged)
        {
            //Walker tree to check rule
            _treeWalker.Walk(_listener, _fileTree[filePathChanged]);
        }

        public void SetListener(IParseTreeListener listener)
        {
            _listener = listener ?? throw new ArgumentNullException("Listener must have value");
        }
    }
}
