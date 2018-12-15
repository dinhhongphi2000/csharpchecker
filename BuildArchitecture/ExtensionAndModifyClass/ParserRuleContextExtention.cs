using Antlr4.Runtime;
using System;

namespace BuildArchitecture
{
    public static class ParserRuleContextExtension
    {
        public static bool InRule(this ParserRuleContext parserContext, RuleContextType contextType)
        {
            RuleContext current = parserContext;
            while(current != null)
            {
                if (current.GetType().Name.ToUpper() == contextType.ToString())
                {
                    return true;
                }
                current = current.Parent;
            }
            return false;
        }

        public static RuleContext GoOutRule(this ParserRuleContext parserContext, RuleContextType contextType)
        {
            RuleContext current = parserContext;
            while (current != null)
            {
                if (current.GetType().Name.ToUpper() == contextType.ToString())
                {
                    return current;
                }
                current = current.Parent;
            }
            return null;
        }
        public static bool InRule(this ParserRuleContext parserContext, Type contextType)
        {
            RuleContext current = parserContext;
            while (current != null)
            {
                if (current.GetType().Name == contextType.Name)
                {
                    return true;
                }
                current = current.Parent;
            }
            return false;
        }
    }
}
