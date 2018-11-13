using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class InterfaceSymbol : DefinitionSymbol
    {
        public List<InterfaceSymbol> BaseTypes { get; set; }
        public List<GenericInfo> GenericParameters { get; set; }

        public InterfaceSymbol(string name, string fullName, string[] modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }

        public InterfaceSymbol(string name, string fullName, List<string> modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }
    }
}
