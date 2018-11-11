using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class EnumSymbol : DefinitionSymbol
    {
        public EnumSymbol(string name, string fullName, string[] modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }

        public EnumSymbol(string name, string fullName, HashSet<string> modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }
    }
}
