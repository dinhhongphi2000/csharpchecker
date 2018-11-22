using System;

namespace BuildArchitecture.Semetic.V2
{
    public class VariableSymbol : BaseSymbol, ITypedSymbol
    {
        public VariableSymbol(string name) : base(name)
        {
        }
    }
}
