using System;

namespace BuildArchitecture.Semetic
{
    public class StructSymbol<T, D> : DefinitionSymbol 
        where T : class
        where D: class
    {
        public StructSymbol(string name, string fullName, string[] modifier = null, string alias = null)
            : base(name, fullName, modifier, alias)
        {
        }
    }
}
