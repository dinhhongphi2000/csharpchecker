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
    public class RuleTwo : BaseRule
    {
        public override void EnterNode([NotNull] ParserRuleContext context)
        {
            base.EnterNode(context);
            if(context is Class_definitionContext)
            {
                Console.WriteLine(context.GetText());
            }
        }

        public override void ExitNode([NotNull] ParserRuleContext context)
        {
            base.ExitNode(context);
            if (context is Class_definitionContext)
            {
                Console.WriteLine("Exit class");
            }
        }
    }
}
