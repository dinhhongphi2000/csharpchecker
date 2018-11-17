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
        public void VisitLiteralContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
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
        public void VisitFor_iteratorContext(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            //check error
            string text = context.GetText();
            if (text.Contains("++"))
            {
                if (start > end)
                {
                    Console.WriteLine("Error");
                    error = new ErrorInformation();
                    error.StartIndex = context.Start.StartIndex;
                    error.Length = context.Stop.StopIndex - context.Start.StartIndex + 1;
                    error.ErrorMessage = "Error";
                }
            }
            else if(text.Contains("--"))
            {
                if (start < end)
                {
                    Console.WriteLine("Error");
                    error = new ErrorInformation();
                    error.StartIndex = context.Start.StartIndex;
                    error.Length = context.Stop.StopIndex - context.Start.StartIndex + 1;
                    error.ErrorMessage = "Error";
                }
            }
        }


    }
}
