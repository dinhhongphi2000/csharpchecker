using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckFieldNameUpperCase
    {
        [Export(typeof(Field_declarationContext))]
        public void CheckField(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Field_declarationContext)context).variable_declarators().variable_declarator()[0].identifier();
            var identifier = identifierContext.GetText();
            var varSymbol = identifierContext.Symbol as FieldSymbol;
            if (varSymbol.HaveModifier("private") && !IsUnderScoreAndLowerCase(identifier))
            {
                error = new ErrorInformation
                {
                    ErrorCode = "IF0007",
                    StartIndex = identifierContext.Start.StartIndex,
                    ReplaceCode = string.Format("_{0}",LowercaseFirst(identifier)),
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                    ErrorMessage = "Private Field member should be begin with _ and lower case character"
                };
            }
            else if((varSymbol.HaveModifier("public") || varSymbol.HaveModifier("protected")) && !IsUpperCase(identifier))
            {
                error = new ErrorInformation
                {
                    ErrorCode = "IF0007",
                    StartIndex = identifierContext.Start.StartIndex,
                    ReplaceCode = UppercaseFirst(identifier),
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                    ErrorMessage = "Public, Protected Field member should be begin with Uppercase character"
                };
            }
        }

        [Export(typeof(Property_declarationContext))]
        public void CheckProperty(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Property_declarationContext)context).member_name().namespace_or_type_name().identifier()[0];
            var identifier = identifierContext.GetText();
            var varSymbol = identifierContext.Symbol as ISymbol;
            if (!IsUpperCase(identifier))
            {
                error = new ErrorInformation
                {
                    ErrorCode = "IF0008",
                    StartIndex = identifierContext.Start.StartIndex,
                    ReplaceCode = UppercaseFirst(identifier),
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                    ErrorMessage = "Property Name should be begin with Uppercase Character"
                };
            }
        }

        private static bool IsUnderScoreAndLowerCase(string s)
        {
            if (s[0] == '_')
            {
                var str = s.TrimStart('_');
                if (LowercaseFirst(str) == str) return true;
                else return false;
            }
            else return false;
        }

        private static bool IsUpperCase(string s)
        {
            s = s.Trim('_');
            if (UppercaseFirst(s) == s) return true;
            else return false;
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
