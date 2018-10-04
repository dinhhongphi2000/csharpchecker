using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BuildArchitecture
{
    internal sealed class NodeVisitedListener : CSharpParserBaseListener
    {
        private Dictionary<RuleContextType, EventHandler<ParserRuleContext>> _eventList = new Dictionary<RuleContextType, EventHandler<ParserRuleContext>>();

        private static NodeVisitedListener _instance = null;
        public static NodeVisitedListener Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NodeVisitedListener();
                }
                return _instance;
            }
            private set { }
        }
        private NodeVisitedListener()
        {
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            try
            {
                string contextTypeName = context.GetType().Name.ToUpper();
                RuleContextType contextType = (RuleContextType)Enum.Parse(typeof(RuleContextType), contextTypeName);
                _eventList[contextType].Invoke(null, context);
            }catch(Exception ex)
            {

            }
        }

        public void RegisterEventContext(RuleContextType contextType, Action<ParserRuleContext> method)
        {
            Debug.WriteLine("Regiser method {0} for visit event context {1}", method.Method.Name, contextType.ToString());
            if (_eventList.ContainsKey(contextType))
            {
                _eventList[contextType] += (sender, context) =>
                {
                    method(context);
                };
            }
            else
            {
                _eventList.Add(contextType, (sender, context) =>
                {
                    method(context);
                });
            }
        }
    }
}
