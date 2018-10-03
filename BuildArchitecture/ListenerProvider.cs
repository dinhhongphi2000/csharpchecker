using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;

namespace BuildArchitecture
{
    internal sealed class ListenerProvider : CSharpParserBaseListener
    {
        public delegate void VisitNode([NotNull] ParserRuleContext context);
        public event VisitNode EnterNode;
        public event VisitNode ExitNode;

        private static ListenerProvider _instance = null;
        public static ListenerProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListenerProvider();
                }
                return _instance;
            }
            private set { }
        }
        private ListenerProvider()
        {
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            //trigger rule event
            OnEnterNode(context);
        }

        public override void ExitEveryRule([NotNull] ParserRuleContext context)
        {
            base.ExitEveryRule(context);
            //trigger rule event
            OnExitNode(context);
        }

        private void OnEnterNode([NotNull] ParserRuleContext context)
        {
            if (EnterNode != null)
            {
                foreach (VisitNode method in EnterNode.GetInvocationList())
                {
                    try
                    {
                        method(context);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }

        private void OnExitNode([NotNull] ParserRuleContext context)
        {
            if (ExitNode != null)
            {
                foreach (VisitNode method in ExitNode.GetInvocationList())
                {
                    try
                    {
                        method(context);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
