using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class DelegateSymbol : DefinitionSymbol
    {
        public List<GenericInfo> GenericParameters { get; set; }

        public DelegateSymbol(string name, string fullName, string[] modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {

        }

        public DelegateSymbol(string name, string fullName, List<string> modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }
    }
}
