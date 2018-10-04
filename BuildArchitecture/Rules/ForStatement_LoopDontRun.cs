using Antlr4.Runtime;
using System;

namespace BuildArchitecture.Rules
{
    class ForStatement_LoopDontRun : BaseRule
    {
        int start;
        int end;

        public override void SetupRuleInfo()
        {
            this.RegisterRule(RuleContextType.LITERALCONTEXT, VisitLiteralContext);
            this.RegisterRule(RuleContextType.FOR_ITERATORCONTEXT, VisitFor_iteratorContext);
        }

        public void VisitLiteralContext(ParserRuleContext context)
        {
            if (context.InRule(RuleContextType.LOCAL_VARIABLE_DECLARATORCONTEXT))
            {
                start = int.Parse(context.GetText());
            }
            if (context.InRule(RuleContextType.FOR_CONDITIONCONTEXT))
            {
                end = int.Parse(context.GetText());
            }
        }

        public void VisitFor_iteratorContext(ParserRuleContext context)
        {
            string text = context.GetText();
            if (text.Contains("++"))
            {
                if (start > end)
                {
                    Console.WriteLine("Error");
                }
            }
            else
            {
                if (start < end)


                {
                    Console.WriteLine("Error");
                }
            }
        }


    }
}
