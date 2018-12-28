using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckEventDelegateNameConvention
    {
        [Export(typeof(Delegate_definitionContext))]
        public void CheckDelegate(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Delegate_definitionContext)context).identifier();
            var identifier = identifierContext.GetText();
            if (!IsUpperCase(identifier))
            {
                var errorMessage = "UIT: Naming rule violation: Delegate should begin with upper case characters";
                error = ThrowError(identifierContext, identifier, "IF0009", errorMessage);
            }
        }

        [Export(typeof(Event_declarationContext))]
        public void CheckEvent(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Event_declarationContext)context).variable_declarators().variable_declarator(0).identifier(); ;
            var identifier = identifierContext.GetText();
            if (!IsUpperCase(identifier))
            {
                var errorMessage = "UIT: Naming rule violation: Event should begin with upper case characters";
                error = ThrowError(identifierContext, identifier, "IF0010", errorMessage);
            }
        }

        private ErrorInformation ThrowError(ParserRuleContext context, string identifier, string errorCode, string errorMessage)
        {
            List<ReplaceCodeInfomation> replaceCodes = new List<ReplaceCodeInfomation>() {
                        new ReplaceCodeInfomation(){
                            Start = context.Start.StartIndex,
                            Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                            ReplaceCode = UppercaseFirst(identifier)
                        }
                    };
            return new ErrorInformation
            {
                ErrorCode = errorCode,
                StartIndex = context.Start.StartIndex,
                DisplayText = string.Format("Rename {0} to {1}", identifier, replaceCodes[0].ReplaceCode),
                ReplaceCode = replaceCodes,
                Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                ErrorMessage = errorMessage
            };
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
