using Antlr4.Runtime;
using System;
using System.ComponentModel.Composition;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class ForStatement_LoopDontRun
    {
        int start;
        int end;

        [Export(typeof(LiteralContext))]
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

        [Export(typeof(For_iteratorContext))]
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
