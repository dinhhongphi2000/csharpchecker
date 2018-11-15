namespace BuildArchitecture.Semetic.V2
{
    /** A scope object typically associated with {...} code blocks */
    public class LocalScope : BaseScope
    {

        public LocalScope(IScope enclosingScope) : base(enclosingScope)
        {
        }

        public override string GetName()
        {
            return "local";
        }
    }
}
