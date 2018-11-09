using System.Collections.Generic;
using Antlr4.Runtime;

namespace BuildArchitecture.Semetic
{
    public class DeclarationSymbol : Symbol
    {
        public virtual DefinitionSymbol Type { get; set; }
        //True, if return Type or Type of variable is pure array (have [])
        public virtual bool IsArray { get; set; }

        public DeclarationSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {

        }

        public DeclarationSymbol(string name, List<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
