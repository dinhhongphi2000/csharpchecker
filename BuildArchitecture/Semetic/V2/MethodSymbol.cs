using System;

namespace BuildArchitecture.Semetic.V2
{
    public class MethodSymbol : Symbol, IType
    {
        public MethodSymbol(string name, IType type) : base(name, type)
        {
        }

        public string GetName()
        {
            return Name;
        }
    }
}
