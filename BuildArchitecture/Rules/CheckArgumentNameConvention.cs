using Antlr4.Runtime;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckArgumentNameConvention
    {
        string _identifier;

        [Export(typeof(Arg_declarationContext))]
        public void sisitIdentifierContext2(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Arg_declarationContext)context).identifier();
            if (identifierContext != null)
            {
                _identifier = identifierContext.GetText();
                if (_identifier == UppercaseFirst(_identifier))
                {
                    List<ReplaceCodeInfomation> replaceCodes = new List<ReplaceCodeInfomation>() {
                        new ReplaceCodeInfomation(){
                            Start = identifierContext.Start.StartIndex,
                            Length = identifierContext.Stop.StopIndex - context.Start.StartIndex + 1,
                            ReplaceCode = LowercaseFirst(_identifier)
                        }
                    };
                    error = new ErrorInformation
                    {
                        StartIndex = identifierContext.Start.StartIndex,
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                        ErrorCode = "IF0006",
                        DisplayText = string.Format("Fix name violation {0}", replaceCodes[0].ReplaceCode),
                        ReplaceCode = replaceCodes,
                        ErrorMessage = "UIT: Naming rule violation: argument should begin with lower case characters."
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
