using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class SuggestNameBoolVariable
    {
        [Export(typeof(Local_variable_declaratorContext))]
        public void CheckVariableInLocal(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Local_variable_declaratorContext)context).identifier();
            var varSymbol = identifierContext.Symbol as VariableSymbol;
            if (varSymbol != null && (varSymbol.GetSymbolType().GetName() == "bool") && !IsStartWith(identifierContext.GetText()))
            {
                error = new ErrorInformation()
                {
                    ErrorCode = "IF0003",
                    ErrorMessage = "Name with bool return type should begin with Is, Can, Has",
                    StartIndex = identifierContext.Start.StartIndex,
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1
                };
            }
        }

        [Export(typeof(Variable_declaratorContext))]
        public void CheckVariableInGlobal(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var identifierContext = ((Variable_declaratorContext)context).identifier();
            var varSymbol = identifierContext.Symbol as FieldSymbol;
            var a = varSymbol.GetSymbolType().GetName();
            if (varSymbol != null && (varSymbol.GetSymbolType().GetName() == "bool") && !IsStartWith(identifierContext.GetText()))
            {
                error = new ErrorInformation()
                {
                    ErrorCode = "IF0003",
                    ErrorMessage = "Name with bool return type should begin with Is, Can, Has",
                    StartIndex = identifierContext.Start.StartIndex,
                    Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1
                };
            }
        }

        [Export(typeof(IdentifierContext))]
        public void CheckMethod(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            if (context != null && context.InRule(RuleContextType.METHOD_MEMBER_NAMECONTEXT))
            {
                var identifierContext = context as IdentifierContext;
                var varSymbol = identifierContext.Symbol as MethodSymbol;
                if (varSymbol != null && (varSymbol.GetSymbolType().GetName() == "bool") && !IsStartWith(identifierContext.GetText()))
                {
                    error = new ErrorInformation()
                    {
                        ErrorCode = "IF0003",
                        ErrorMessage = "Name with bool return type should begin with Is, Can, Has",
                        StartIndex = identifierContext.Start.StartIndex,
                        Length = identifierContext.Stop.StopIndex - identifierContext.Start.StartIndex + 1
                    };
                }
            }
        }

        private bool IsStartWith(string identifier)
        {
            identifier = identifier.ToLower();
            string[] str = { "is", "can", "has" };
            for (int i = 0; i < str.Length; i++)
            {
                if (identifier.StartsWith(str[i])) return true;
            }
            return false;
        }
    }
}
