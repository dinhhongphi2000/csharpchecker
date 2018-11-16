using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class GenericInfo
    {
        public string DeclareType { get; set; }
        public HashSet<string> ActualType { get; set; }
        public HashSet<string> ExpectType { get; set; }
    }
}
