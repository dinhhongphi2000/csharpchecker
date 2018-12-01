namespace BuildArchitecture.Semetic.V2
{
    /** A scope associated with globals. */
    public class GlobalScope : BaseScope
    {
        public GlobalScope(IScope enclosingScope) : base(enclosingScope)
        {
            InitPrimitiveType();
        }
        public override string GetName() { return "global"; }

        public void InitPrimitiveType()
        {
            Define(new PrimitiveType("bool"));
            Define(new PrimitiveType("byte"));
            Define(new PrimitiveType("sbyte"));
            Define(new PrimitiveType("char"));
            Define(new PrimitiveType("decimal"));
            Define(new PrimitiveType("double"));
            Define(new PrimitiveType("float"));
            Define(new PrimitiveType("int"));
            Define(new PrimitiveType("uint"));
            Define(new PrimitiveType("long"));
            Define(new PrimitiveType("ulong"));
            Define(new PrimitiveType("object"));
            Define(new PrimitiveType("short"));
            Define(new PrimitiveType("ushort"));
            Define(new PrimitiveType("string"));
        }
    }
}
