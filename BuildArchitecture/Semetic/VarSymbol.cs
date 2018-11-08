namespace BuildArchitecture.Semetic
{
    public class VarSymbol : DeclarationSymbol
    {
        public VarSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }
    }
}
