using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace BuildArchitecture
{
    public static class GetChildConext
    {
        public static List<T> GetDeepChildContext<T>(this ParserRuleContext context)
            where T : ParserRuleContext
        {
            ContextListener<T> listener = new ContextListener<T>();
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(listener, context);
            return listener.ContextList;
        }
    }

    public class ContextListener<T> : CSharpParserBaseListener where T : ParserRuleContext
    {
        public List<T> ContextList { get; set; }

        public ContextListener()
        {
            ContextList = new List<T>();
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            if (context is T)
                ContextList.Add((T)context);
            base.EnterEveryRule(context);
        }
    }
}
