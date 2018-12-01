using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture.Semetic.V2
{
    public class SemeticAnalysis
    {
        private readonly LinkerScopeCollection linker;
        //Walker which visits node of Parser tree and call action of listener
        private ParseTreeWalker treeWalker = null;

        public SemeticAnalysis()
        {
            treeWalker = new ParseTreeWalker();
            linker = new LinkerScopeCollection();
        }

        /// <summary>
        /// Run two phrase
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="context"></param>
        public void Run(string fileName, ParserRuleContext context)
        {
            RunDefinePhrase(fileName, context);
        }

        public void RunDefinePhrase(string fileName, ParserRuleContext context)
        {
            DefineSymbolPhrase analysis = new Semetic.V2.DefineSymbolPhrase(fileName, linker);
            treeWalker.Walk(analysis, context);
        }

        public void RunResolvePhrase(ParserRuleContext context)
        {
            ResolveSymbolPhrase resolvePhrase = new ResolveSymbolPhrase();
            resolvePhrase.Visit(context);
        }
    }
}
