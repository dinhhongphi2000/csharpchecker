using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class DefinitionSymbol : Symbol
    {
        public string FullName { get; set; }
        public string Namespace { get; set; }
        public List<VarSymbol> Properties { get; set; }
        public List<FuncSymbol> Functions { get; set; }

        public DefinitionSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {

        }

        public DefinitionSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
