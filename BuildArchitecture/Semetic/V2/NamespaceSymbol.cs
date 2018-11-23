namespace BuildArchitecture.Semetic.V2
{
    public class NamespaceSymbol : SymbolWithScope
    {
        public NamespaceSymbol(string name) : base(name)
        {
        }

        public NamespaceSymbol(string name, IScope enclosingScope) : base(name)
        {
            base.SetEnclosingScope(enclosingScope);
        }
    }
}
