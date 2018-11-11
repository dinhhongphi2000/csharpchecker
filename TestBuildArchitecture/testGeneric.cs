using System;

namespace BuildArchitecture.Semetic
{
    public class StructSymbol<T, D> : DefinitionSymbol, IABC<T>, IAB
        where T : class
        where D: class, IAB
    {
        public StructSymbol(string name, string fullName, string[] modifier = null, string alias = null)
            : base(name, fullName, modifier, alias)
        {
        }
    }

    public interface IABC<X> { }
    public interface IAB { }
    class XYZ { }
}
