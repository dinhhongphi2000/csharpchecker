using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class DelegateSymbol : DefinitionSymbol
    {
        public List<GenericInfo> GenericParameters { get; set; }

        public DelegateSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {

        }

        public DelegateSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
