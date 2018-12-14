using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckArrayConstantInit
    {
        int _value;
        [Export(typeof(LiteralContext))]
        public void CheckIfInsideHasNumber(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            if (context.InRule(RuleContextType.EXPRESSION_LISTCONTEXT) || context.InRule(RuleContextType.ARGUMENT_LISTCONTEXT))
            {
                if (int.TryParse(context.GetText(), out _value))
                {
                    error = new ErrorInformation()
                    {
                        ErrorCode = "WA0002",
                        ErrorMessage = "List or array with specified number could make overfloat",
                        StartIndex = context.Start.StartIndex,
                        Length = context.Stop.StopIndex - context.Start.StartIndex + 1
                    };
                }
            }
            
        }

    }
}
