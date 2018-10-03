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
        public override void EnterNode([NotNull] ParserRuleContext context)
        {
            base.EnterNode(context);
            if (!(context is ForStatementContext)) return;
            var a = context as ForStatementContext;
            Console.WriteLine(a?.for_initializer()?.GetChild(0).GetChild(1).GetChild(2).GetText());
        }
    }
}
