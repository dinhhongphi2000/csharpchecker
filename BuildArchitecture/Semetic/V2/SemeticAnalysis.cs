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
        private List<ErrorInformation> errorTable;

        public SemeticAnalysis()
        {
            treeWalker = new ParseTreeWalker();
            linker = new LinkerScopeCollection();
            errorTable = new List<ErrorInformation>();
        }

        public void SetErrorTable(List<ErrorInformation> errorTable)
        {
            this.errorTable = errorTable ?? throw new ArgumentNullException();
        }

        public List<ErrorInformation> GetErrors()
        {
            return errorTable;
        }

        /// <summary>
        /// Run two phrase
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="context"></param>
        public void Run(string fileName, ParserRuleContext context)
        {
            errorTable.Clear();
            RunDefinePhrase(fileName, context);
            RunResolvePhrase(context);
        }

        public void RunDefinePhrase(string fileName, ParserRuleContext context)
        {
            errorTable.Clear();
            DefineSymbolPhrase analysis = new Semetic.V2.DefineSymbolPhrase(fileName, linker);
            treeWalker.Walk(analysis, context);
        }

        public void RunResolvePhrase(ParserRuleContext context)
        {
            errorTable.Clear();
            ResolveSymbolPhrase resolvePhrase = new ResolveSymbolPhrase();
            resolvePhrase.SetErrorTable(errorTable);
            resolvePhrase.Visit(context);
            errorTable = resolvePhrase.GetErrors();
        }
    }
}
