using Antlr4.Runtime;
using System.Collections.Generic;
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
                    List<ReplaceCodeInfomation> replaceCode = new List<ReplaceCodeInfomation>() {
                        new ReplaceCodeInfomation(){
                            Start = context.Start.StartIndex,
                            Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                            ReplaceCode = LowercaseFirst(identifier)
                        }
                    };
                    error = new ErrorInformation
                    {
                        StartIndex = context.Start.StartIndex,
                        ErrorCode = "IF0005",
                        ReplaceCode = replaceCode,
                        Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                        ErrorMessage = "Local variable name should not be upper case"
                    };
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
                    List<ReplaceCodeInfomation> replaceCode = new List<ReplaceCodeInfomation>() {
                        new ReplaceCodeInfomation(){
                            Start = context.Start.StartIndex,
                            Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                            ReplaceCode = LowercaseFirst(identifier)
                        }
                    };
                    error = new ErrorInformation
                    {
                        StartIndex = context.Start.StartIndex,
                        Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                        ErrorCode = "IF0006",
                        ReplaceCode = replaceCode,
                        ErrorMessage = "Argument should not be upper case"
                    };
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
        static string LowercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToLower(s[0]) + s.Substring(1);
        }
    }
}
