﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildArchitecture
{
    public class WorkSpace
    {
        //Listener is called when walker visit node. Responsibility for listener is loading all rule in folder or assembly
        private RuleChecker _scanTree = null;
        //Walker which visits node of Parser tree and call action of listener
        private ParseTreeWalker _treeWalker = null;
        private SemeticAnalysis analysis;
        private Dictionary<string, List<ErrorInformation>> errorTable = new Dictionary<string, List<ErrorInformation>>(); //key file, value ErrorList
        private Dictionary<string, ITokenStream> tokenStreams = new Dictionary<string, ITokenStream>();
#if TEST
        public Dictionary<string, ParserRuleContext> _parserRuleContextOfFile;
#else
        private Dictionary<string, ParserRuleContext> _parserRuleContextOfFile;
#endif
        private static readonly Lazy<WorkSpace> lazy =
            new Lazy<WorkSpace>(() => new WorkSpace());

        public static WorkSpace Instance { get { return lazy.Value; } }
        public ParseTreeWalker TreeWalker
        {
            get { return _treeWalker; }
            private set { }
        }

        private WorkSpace()
        {
            //Initial default value
            _parserRuleContextOfFile = new Dictionary<string, ParserRuleContext>();
            _scanTree = new RuleChecker();
            _treeWalker = new ParseTreeWalker();
            analysis = new SemeticAnalysis();
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
            tokenStreams[filePath] = tokens;
        }

        public void RunSemeticAnalysis(string filePath)
        {
            analysis.Run(filePath, _parserRuleContextOfFile[filePath]);
        }

        public void RunDefinedPhraseAllfile()
        {
            foreach (var item in _parserRuleContextOfFile)
            {
                analysis.RunDefinePhrase(item.Key, item.Value);
            }
        }

        public void RunResolvePhraseAllFile()
        {
            foreach (var item in _parserRuleContextOfFile)
            {
                analysis.RunResolvePhrase(item.Value);
            }
        }

        /// <summary>
        /// Run semetic and rule for specific file
        /// </summary>
        /// <param name="filePath"></param>
        public void RunRules(string filePath)
        {
                //clear old error of this filePath
                if (errorTable.ContainsKey(filePath))
                    errorTable[filePath].Clear();
                else
                    errorTable[filePath] = new List<ErrorInformation>();
                ParserRuleContext tree = _parserRuleContextOfFile[filePath];
                //try
                //{
                //Run semetic 
                analysis.Run(filePath, tree);
                errorTable[filePath].AddRange(analysis.GetErrors());
                //}
                //catch (Exception ex)
                //{

                //}

                //Run rule
                //Walker tree to check rule and add error to error list
                _scanTree.SetCurrentTokenStream(tokenStreams[filePath]);
                _treeWalker.Walk(_scanTree, tree);
                errorTable[filePath].AddRange(_scanTree.GetErrors());
        }

        /// <summary>
        /// Run semetic and rule for all files
        /// </summary>
        public void RunRulesAllFile()
        {
                errorTable.Clear();
                foreach (var item in _parserRuleContextOfFile)
                {
                    RunRules(item.Key);
                }
        }
        /// <summary>
        /// Get error list after run rule
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<ErrorInformation>> GetErrors()
        {
            return errorTable;
        }

        public string FindDuplicateFunction()
        {
            return analysis.FindDuplicateFunction();
        }

        public List<ErrorInformation> GetErrors(string filePath)
        {
            return errorTable[filePath];
        }
    }
}
