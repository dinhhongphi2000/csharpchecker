using System;

namespace BuildArchitecture.Semetic.V2
{
    public class VariableSymbol : Symbol, IType
    {
        public VariableSymbol(string name, IType type) : base(name, type)
        {
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
