using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class InterfaceSymbol : DefinitionSymbol
    {
        public List<InterfaceSymbol> BaseTypes { get; set; }
        public List<GenericInfo> GenericParameters { get; set; }

        public InterfaceSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public InterfaceSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
