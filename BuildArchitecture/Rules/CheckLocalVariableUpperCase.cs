using Antlr4.Runtime;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckLocalVariableUpperCase
    {
        string identifier;

        [Export(typeof(IdentifierContext))]
        public void VisitIdentifierContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            if (context.InRule(RuleContextType.LOCAL_VARIABLE_DECLARATORCONTEXT) && !context.InRule(RuleContextType.LOCAL_VARIABLE_INITIALIZERCONTEXT))
            {
                identifier = context.GetText();
                if (identifier == UppercaseFirst(identifier))
                {
                    error = new ErrorInformation();
                    error.StartIndex = context.Start.StartIndex;
                    error.ErrorCode = "IF0005";
                    error.Length = context.Stop.StopIndex - context.Start.StartIndex + 1;
                    error.ErrorMessage = "Local variable name should not be upper case";
                }
            }
        }

        [Export(typeof(IdentifierContext))]
        public void VisitIdentifierContext2(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            if (context.InRule(RuleContextType.ARG_DECLARATIONCONTEXT) && !context.InRule(RuleContextType.TYPECONTEXT))
            {
                identifier = context.GetText();
                if (identifier == UppercaseFirst(identifier))
                {
                    error = new ErrorInformation();
                    error.StartIndex = context.Start.StartIndex;
                    error.Length = context.Stop.StopIndex - context.Start.StartIndex + 1;
                    error.ErrorMessage = "Dont put your variable name Uppercase";
                }
            }
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
