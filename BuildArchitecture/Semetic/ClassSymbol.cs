using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class ClassSymbol : DefinitionSymbol
    {
        public List<DefinitionSymbol> BaseTypes { get; set; }
        public List<GenericInfo> GenericParameters { get; set; }

        public ClassSymbol(string name, string[] modifier = null, string alias = null)
            : base(name, modifier, alias)
        {
        }

        public ClassSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
