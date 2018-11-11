using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class EventSymbol : DeclarationSymbol
    {
        public EventSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public EventSymbol(string name, HashSet<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
