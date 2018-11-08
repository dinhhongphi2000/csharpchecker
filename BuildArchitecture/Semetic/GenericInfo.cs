using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class GenericInfo
    {
        public string DeclareType { get; set; }
        public DefinitionSymbol ActualType { get; set; }
        public List<DefinitionSymbol> AllowType { get; set; }
    }
}
