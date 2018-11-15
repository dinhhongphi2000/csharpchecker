namespace BuildArchitecture.Semetic.V2
{
    /** A scope associated with globals. */
    public class GlobalScope : BaseScope
    {
        public GlobalScope(IScope scope) : base(scope) { }
        public override string GetName() { return "global"; }
    }
}
