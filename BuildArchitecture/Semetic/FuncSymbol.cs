using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class FuncSymbol : DeclarationSymbol
    {
        public HashSet<string> Parameters { get; set; }
        public HashSet<GenericInfo> GenericParameters { get; set; }

        public FuncSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public FuncSymbol(string name, HashSet<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
