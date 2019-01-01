using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class LockStatementWarning
    {
        [Export(typeof(LockStatementContext))]
        public void CheckLockStatement(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            var priExpressionContext = context.GetDeepChildContext<Primary_expressionContext>()[0];
            if(priExpressionContext.GetText() == "this")
            {
                error = ThrowError(priExpressionContext);

            }
            else 
            {
                var typeofContext = priExpressionContext.GetDeepChildContext<TypeofExpressionContext>();
                if(typeofContext.Count > 0)
                {
                    error = ThrowError(priExpressionContext);
                }
            }
        }
        public ErrorInformation ThrowError(ParserRuleContext context)
        {
           return new ErrorInformation
            {
                StartIndex = context.Start.StartIndex,
                ErrorCode = "WA0006",
                Length = context.Stop.StopIndex - context.Start.StartIndex + 1,
                ErrorMessage = "UIT: Types and this keyword should not be used for locking "
           };
        }
    }
}
