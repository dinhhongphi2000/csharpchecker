using Antlr4.Runtime;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckLocalVariableNameConvention
    {
        string _identifier;

        [Export(typeof(Local_variable_declaratorContext))]
        public void VisitIdentifierContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Local_variable_declaratorContext)context).identifier();
            if (identifierContext != null)
            {
                _identifier = identifierContext.GetText();
                if (!IsLowerCase(_identifier) || _identifier[0] == '_')
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
                        ErrorCode = "IF0005",
                        ReplaceCode = replaceCodes,
                        DisplayText = string.Format("Fix name violation: {0}", replaceCodes[0].ReplaceCode),
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                        ErrorMessage = "UIT: Naming rule violation: Local variable should begin with lower case characters."
                    };
                }
            }
        }

        private static bool IsLowerCase(string s)
        {
            s = s.Replace("_", "");
            if (LowercaseFirst(s) == s) return true;
            else return false;
        }

        static string LowercaseFirst(string s)
        {
            s = s.Replace("_","");
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
