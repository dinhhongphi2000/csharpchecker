using Antlr4.Runtime;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckVariableNameAllUpperCase
    {
        string identifier;

        [Export(typeof(IdentifierContext))]
        public void VisitIdentifierContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
                identifier = context.GetText();
                if (identifier == identifier.ToUpper() && identifier.Length > 2)
                {
                error = new ErrorInformation
                {
                    ErrorCode = "IF0004",
                    StartIndex = context.Start.StartIndex,
                    Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                    ErrorMessage = "Variable name should not all upper case"
                };
            }
        }
    }
}
