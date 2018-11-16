using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class InterfaceSymbol : DefinitionSymbol
    {
        public HashSet<string> BaseTypes { get; set; }
        public HashSet<GenericInfo> GenericParameters { get; set; }

        public InterfaceSymbol(string name, string fullName, string[] modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }

        public InterfaceSymbol(string name, string fullName, HashSet<string> modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }
    }
}
