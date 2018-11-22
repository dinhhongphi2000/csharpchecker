using Antlr4.Runtime;

namespace BuildArchitecture
{
    public static class ParserRuleContextExtension
    {
        public static bool InRule(this ParserRuleContextWithScope parserContext, RuleContextType contextType)
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
    }
}
