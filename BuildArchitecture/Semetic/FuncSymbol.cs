using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class FuncSymbol : DeclarationSymbol
    {
        public List<VarSymbol> Parameters { get; set; }
        public List<GenericInfo> GenericParameters { get; set; }

        public FuncSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public FuncSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
