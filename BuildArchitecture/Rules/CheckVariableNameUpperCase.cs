using Antlr4.Runtime;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckVariableNameUpperCase
    {
        string identifier;

        [Export(typeof(IdentifierContext))]
        public void VisitIdentifierContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
                identifier = context.GetText();
                if (identifier == identifier.ToUpper())
                {
                    error = new ErrorInformation();
                    error.ErrorCode = "IF0004";
                    error.StartIndex = context.Start.StartIndex;
                    error.Length = context.Stop.StopIndex - context.Start.StartIndex + 1;
                    error.ErrorMessage = "Variable name should not all upper case";
                }
        }
    }
}
