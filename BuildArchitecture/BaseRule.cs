using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture
{
    public class BaseRule
    {
        private static ListenerProvider _provider;
        public BaseRule()
        {
            _provider = ListenerProvider.Instance;
            _provider.EnterNode += EnterNode;
            _provider.ExitNode += ExitNode;
        }

        public virtual void EnterNode([Antlr4.Runtime.Misc.NotNull] Antlr4.Runtime.ParserRuleContext context) { }

        public virtual void ExitNode([Antlr4.Runtime.Misc.NotNull] Antlr4.Runtime.ParserRuleContext context) { }
    }
}
