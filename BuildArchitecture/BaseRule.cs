using Antlr4.Runtime;
using System;

namespace BuildArchitecture
{
    public abstract class BaseRule
    {

        private static NodeVisitedListener _listenter;
        public BaseRule()
        {
            _listenter = NodeVisitedListener.Instance;
        }

        public abstract void SetupRuleInfo();

        public void RegisterRule(RuleContextType contextType, Action<ParserRuleContext> method)
        {
            _listenter.RegisterEventContext(contextType, method);
        }
    }
}
