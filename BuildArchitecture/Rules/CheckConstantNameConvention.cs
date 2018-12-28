using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckConstantNameConvention
    {
        [Export(typeof(Constant_declaratorContext))]
        public void CheckConstant(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Constant_declaratorContext)context).identifier();
            var identifier = identifierContext.GetText();
            if (!IsUpperCase(identifier) || identifier[0] == '_')
            {
                List<ReplaceCodeInfomation> replaceCodes = new List<ReplaceCodeInfomation>() {
                        new ReplaceCodeInfomation(){
                            Start = identifierContext.Start.StartIndex,
                            Length = identifierContext.Stop.StopIndex - context.Start.StartIndex + 1,
                            ReplaceCode = UppercaseFirst(identifier)
                        }
                    };
                error = new ErrorInformation
                {
                    ErrorCode = "IF0008",
                    StartIndex = identifierContext.Start.StartIndex,
                    DisplayText = string.Format("Fix name violation {0}", replaceCodes[0].ReplaceCode),
                    ReplaceCode = replaceCodes,
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                    ErrorMessage = "UIT: Naming rule violation: Constant should begin with upper case characters"
                };
            }
        }

        private static bool IsUpperCase(string s)
        {
            s = s.Replace("_","");
            if (UppercaseFirst(s) == s) return true;
            else return false;
        }
        static string UppercaseFirst(string s)
        {
            s = s.Replace("_", "");
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
