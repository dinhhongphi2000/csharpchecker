namespace BuildArchitecture.Semetic.V2
{
    public class PropertySymbol : Symbol, IType
    {
        public PropertySymbol(string name, IType type) : base(name, type)
        {
        }

        public string GetName()
        {
            return Name;
        }
    }
}
