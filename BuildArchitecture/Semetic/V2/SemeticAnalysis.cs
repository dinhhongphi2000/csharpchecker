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
        private bool allowSubPhraseClearErrorTable = true;

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
            allowSubPhraseClearErrorTable = false;
            RunDefinePhrase(fileName, context);
            RunResolvePhrase(context);
        }

        public void RunDefinePhrase(string fileName, ParserRuleContext context)
        {
            if (allowSubPhraseClearErrorTable == true)
                errorTable.Clear();
            DefineSymbolPhrase analysis = new Semetic.V2.DefineSymbolPhrase(fileName, linker);
            treeWalker.Walk(analysis, context);
            errorTable = analysis.GetErrors();
        }

        public void RunResolvePhrase(ParserRuleContext context)
        {
            if (allowSubPhraseClearErrorTable == true)
                errorTable.Clear();
            ResolveSymbolPhrase resolvePhrase = new ResolveSymbolPhrase();
            resolvePhrase.SetErrorTable(errorTable);
            resolvePhrase.Visit(context);
            errorTable = resolvePhrase.GetErrors();
        }

        public string FindDuplicateFunction()
        {
            List<ISymbol> methodSymbols = new List<ISymbol>();
            foreach(var key in linker.GetKeys())
            {
                foreach(var value in linker[key])
                {
                    if(value is ClassSymbol || value is StructSymbol)
                    {
                        var symbol = value.GetAllSymbols().FindAll(s => s is MethodSymbol);
                        methodSymbols.AddRange(symbol);
                    }
                }
            }

            if(methodSymbols.Count > 1)
            {
                var duplicateGroup = methodSymbols.GroupBy(x =>
                {
                    var identifierContext = (x as MethodSymbol).DefNode;
                    var functionContext = identifierContext.Parent.Parent.Parent;
                    return functionContext.GetText();
                }).Where(g => g.Count() > 1)
                .Select(y => y)
                .ToList();
                if(duplicateGroup.Count > 0)
                {
                    string errorList = "";
                    foreach(var group in duplicateGroup)
                    {
                        var duplicateSymbols = group.ToList();
                        string error = "";
                        error += "\r\nFind duplicate function";
                        error += "\r\n\tFunction " + duplicateSymbols[0].GetName() + " at " + duplicateSymbols[0].GetFullyQualifiedName(".").Remove(0,"global.".Length);
                        error += "\r\n\tFunction " + duplicateSymbols[1].GetName() + " at " + duplicateSymbols[1].GetFullyQualifiedName(".").Remove(0, "global.".Length);
                        errorList += error;
                    }
                    return errorList;
                }
            }
            return "Not Found Duplicate function";
        }
    }
}
