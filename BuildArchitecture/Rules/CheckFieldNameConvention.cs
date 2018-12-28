using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckFieldNameConvention
    {
        [Export(typeof(Field_declarationContext))]
        public void CheckField(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Field_declarationContext)context).variable_declarators().variable_declarator()[0].identifier();
            var identifier = identifierContext.GetText();
            var varSymbol = identifierContext.Symbol as FieldSymbol;
            if (varSymbol.HaveModifier("private") || varSymbol.HaveModifier("internal"))
            {
                if (!IsUnderScoreAndLowerCase(identifier))
                {
                    List<ReplaceCodeInfomation> replaceCodes = new List<ReplaceCodeInfomation>() {
                        new ReplaceCodeInfomation(){
                            Start = identifierContext.Start.StartIndex,
                            Length = identifierContext.Stop.StopIndex - context.Start.StartIndex + 1,
                            ReplaceCode = string.Format("_{0}",LowercaseFirst(identifier))
                        }
                    };
                    error = new ErrorInformation
                    {
                        ErrorCode = "IF0007",
                        DisplayText = string.Format("Rename {0} to {1}", identifier, replaceCodes[0].ReplaceCode),
                        StartIndex = identifierContext.Start.StartIndex,
                        ReplaceCode = replaceCodes,
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                        ErrorMessage = "UIT: Naming rule violation: private, internal field member should be begin with _ and lower case character"
                    };
                }
            }
            else if ((varSymbol.HaveModifier("public") || varSymbol.HaveModifier("protected")) )
            {
                if (!IsUpperCase(identifier))
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
                        ErrorCode = "IF0011",
                        StartIndex = identifierContext.Start.StartIndex,
                        DisplayText = string.Format("Rename {0} to {1}", identifier, replaceCodes[0].ReplaceCode),
                        ReplaceCode = replaceCodes,
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                        ErrorMessage = "UIT: Naming rule violation: public, protected Field member should be begin with uppercase character "
                    };
                }
            }
        }

        private static bool IsUnderScoreAndLowerCase(string s)
        {
            if (s[0] == '_')
            {
                var str = s.Replace("_","");
                if (LowercaseFirst(str) == str) return true;
                else return false;
            }
            else return false;
        }

        private static bool IsUpperCase(string s)
        {
            if (s[0] == '_') return false;
            if (UppercaseFirst(s) == s) return true;
            else return false;
        }
        static string UppercaseFirst(string s)
        {
            s = s.Replace("_","");
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
            s = s.Replace("_", "");
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
