using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class EnumSymbol : DefinitionSymbol
    {
        public EnumSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public EnumSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
