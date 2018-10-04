using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Rules
{
    class ForStatement_LoopDontRun : BaseRule
    {
        int start;
        int end;
        public override void EnterNode([NotNull] ParserRuleContext context)
        {
            if(context is LiteralContext)
            {
                if(context.InRule(RuleContextType.LOCAL_VARIABLE_DECLARATORCONTEXT))
                {
                    start = int.Parse(context.GetText());
                }
                if (context.InRule(RuleContextType.FOR_CONDITIONCONTEXT))
                {
                    end = int.Parse(context.GetText());
                }
            }
            if(context is For_iteratorContext)
            {
                string text = context.GetText();
                if (text.Contains("++"))
                {
                    if(start > end)
                    {
                        Console.WriteLine("Error");
                    }
                }
                else
                {
                    if(start < end)


                    {
                        Console.WriteLine("Error");
                    }
                }
            }
        }
    }
}
