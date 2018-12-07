using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckVariableOverrideValue
    {
        [Export(typeof(Local_variable_declaratorContext))]
        public void CheckError(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Local_variable_declaratorContext)context).identifier();
            var currentScope = identifierContext.Scope.GetEnclosingScope();
            var text = identifierContext.GetText();
            //check symbol exist
           var existSymbol = currentScope.Resolve(identifierContext.GetText());
            if (existSymbol != null && existSymbol.GetScope().GetName() != "local")
            {
                //warning
                error = new ErrorInformation()
                {
                    ErrorCode = "WA0001",
                    ErrorMessage = "You should declare variable " + identifierContext.GetText() + " with difference name to avoid override value",
                    StartIndex = identifierContext.Start.StartIndex,
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1
                };
            }
        }
    }
}
