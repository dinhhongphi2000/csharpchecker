using System;

namespace BuildArchitecture.Semetic.V2
{
    public class VariableSymbol : BaseSymbol, TypedSymbol
    {
        public VariableSymbol(string name) : base(name)
        {
        }

        public void SetType(IType type)
        {
            base.SetType(type);
        }
    }
}
