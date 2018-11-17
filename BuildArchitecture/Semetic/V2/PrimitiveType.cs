namespace BuildArchitecture.Semetic.V2
{
    public class PrimitiveType : BaseSymbol, IType
    {
        protected int typeIndex;

        public PrimitiveType(string name) : base(name)
        {
        }

        public int GetTypeIndex() { return typeIndex; }

        public void SetTypeIndex(int typeIndex)
        {
            this.typeIndex = typeIndex;
        }
    }
}
