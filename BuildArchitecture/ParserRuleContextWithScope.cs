using Antlr4.Runtime.Misc;
using BuildArchitecture.Semetic.V2;

namespace BuildArchitecture
{
    public class ParserRuleContextWithScope : Antlr4.Runtime.ParserRuleContext
    {
        public ISymbol Symbol { get; set; }
        public IScope Scope { get; set; }

        public ParserRuleContextWithScope() : base() { }


        public ParserRuleContextWithScope([Nullable] Antlr4.Runtime.ParserRuleContext parent, int invokingStateNumber)
            : base(parent, invokingStateNumber) { }
    }
}
