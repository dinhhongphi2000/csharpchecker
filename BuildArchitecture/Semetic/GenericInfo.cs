using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class GenericInfo
    {
        public string DeclareType { get; set; }
        public Dictionary<string, string> ActualType { get; set; }
        public Dictionary<string, string> ExpectType { get; set; }
    }
}
