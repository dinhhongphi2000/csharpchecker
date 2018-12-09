using Antlr4.Runtime;

namespace BuildArchitecture.Context
{
    public class RuleContextInfomation
    {
        public ParserRuleContext Context { get; set; }
        public ITokenStream TokenStream { get; set; }
    }
}
