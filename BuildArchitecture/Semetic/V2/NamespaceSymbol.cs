namespace BuildArchitecture.Semetic.V2
{
    public class NamespaceSymbol : DataAggregateSymbol
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
