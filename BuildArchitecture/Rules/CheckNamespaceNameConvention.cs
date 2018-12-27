using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckNamespaceNameConvention
    {
        [Export(typeof(NamespaceContext))]
        public void CheckNamespace(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            IdentifierContext[] identifierContexts = ((NamespaceContext)context).qualified_identifier().identifier();
            foreach (var identifierContext in identifierContexts)
            {
                var identifier = identifierContext.GetText();
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
                        ErrorCode = "IF0012",
                        StartIndex = identifierContext.Start.StartIndex,
                        DisplayText = string.Format("Rename {0} to {1}", identifier, replaceCodes[0].ReplaceCode),
                        ReplaceCode = replaceCodes,
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1,
                        ErrorMessage = "UIT: Naming rule violation: Namespace should begin with upper case characters"
                    };
                }
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
        static string UppercaseFirst(string ss)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(ss))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(ss[0]) + ss.Substring(1);
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
