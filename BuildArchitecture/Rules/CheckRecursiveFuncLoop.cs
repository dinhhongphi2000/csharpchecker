using Antlr4.Runtime;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class CheckRecursiveFuncLoop
    {
        [Export(typeof(Method_declarationContext))]
        public void CheckLoop(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            List<Unary_expressionContext> unaryExpressionContext = ((Method_declarationContext)context).GetDeepChildContext<Unary_expressionContext>();
            var methodContext = ((Method_declarationContext)context);

            foreach (var contxt in unaryExpressionContext)
            {
                    var methodName = methodContext.method_member_name().identifier()[0].GetText();
                if (IsMethod(contxt)  && !HasCheckMethod(methodContext))
                {
                    if (IsSameName(contxt, methodName))
                    {
                        error = new ErrorInformation()
                        {
                            ErrorCode = "WA0005",
                            ErrorMessage = "Recursive function should have conditional statement to stop recursive",
                            StartIndex = context.Start.StartIndex,
                            Length = context.Stop.StopIndex - context.Start.StartIndex + 1
                        };
                    }
                }
            }

        }

        public bool IsMethod(Unary_expressionContext unary_ExpressionContext)
        {
            var methodInvocationContext = unary_ExpressionContext.GetDeepChildContext<Unary_expressionContext>();
            return (methodInvocationContext.Count > 0) ? true : false;
        }

        public bool HasCheckMethod(Method_declarationContext method_DeclarationContext)
        {
            var hasIfStatement = method_DeclarationContext.GetDeepChildContext<IfStatementContext>().Count > 0;
            var hasForeachStatement = method_DeclarationContext.GetDeepChildContext<ForeachStatementContext>().Count > 0;
            var hasWhileStatement = method_DeclarationContext.GetDeepChildContext<WhileStatementContext>().Count > 0;
            var hasForStatement = method_DeclarationContext.GetDeepChildContext<ForStatementContext>().Count > 0;
            if (!hasIfStatement && !hasForeachStatement && !hasWhileStatement && !hasForeachStatement)
            {
                return false;
            }
            else return true;
        }

        public static bool IsSameName(Unary_expressionContext unary_ExpressionContext, string methodName)
        {
            var simpleNameExpression = unary_ExpressionContext.GetDeepChildContext<SimpleNameExpressionContext>();
            var memberAccessContext = unary_ExpressionContext.GetDeepChildContext<Member_accessContext>();

            if (simpleNameExpression.Count > 0)
            {

                var name = simpleNameExpression[0].identifier().GetText();
                if (name == methodName)
                {
                    if (memberAccessContext.Count > 0) return false;
                    else return true;
                }
            }

            if (memberAccessContext.Count > 0)
            {
                var identifier = memberAccessContext[0].identifier();
                return methodName == identifier.GetText() ? true : false;
            }

            return false;
        }
    }
}
