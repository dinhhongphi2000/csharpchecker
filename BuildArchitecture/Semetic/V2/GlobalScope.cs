namespace BuildArchitecture.Semetic.V2
{
    /** A scope associated with globals. */
    public class GlobalScope : BaseScope
    {
        public GlobalScope(IScope enclosingScope) : base(enclosingScope) { }
        public override string GetName() { return "global"; }
    }
}
