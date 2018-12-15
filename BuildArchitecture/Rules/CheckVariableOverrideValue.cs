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
                List<ReplaceCodeInfomation> replaceCodes = new List<ReplaceCodeInfomation>()
                {
                    new ReplaceCodeInfomation()
                    {
                        Start = identifierContext.Start.StartIndex,
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                        ReplaceCode = identifierContext.GetText() + "1"
                    }
                };
                //warning
                error = new ErrorInformation()
                {
                    ErrorCode = "WA0001",
                    ErrorMessage = "You should declare variable " + identifierContext.GetText() + " with difference name to avoid override value",
                    StartIndex = identifierContext.Start.StartIndex,
                    DisplayText = string.Format("Rename {0} to {1}", identifierContext.GetText(),replaceCodes[0].ReplaceCode),
                    ReplaceCode = replaceCodes,
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1
                };
            }
        }
    }
}
