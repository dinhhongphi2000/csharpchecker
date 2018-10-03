using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture
{
    internal sealed class Provider : CSharpParserBaseListener
    {
        public delegate void VisitNode([NotNull] ParserRuleContext context);
        public event VisitNode EnterNode;
        public event VisitNode ExitNode;

        private static Provider _instance = null;
        public static Provider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Provider();
                }
                return _instance;
            }
            private set { }
        }
        private Provider()
        {
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            //trigger rule event
            EnterNode(context);
        }

        public override void ExitEveryRule([NotNull] ParserRuleContext context)
        {
            base.ExitEveryRule(context);
            //trigger rule event
            ExitNode(context);
        }
    }
}
