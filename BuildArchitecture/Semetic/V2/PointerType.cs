namespace BuildArchitecture.Semetic.V2
{
    /** An element in a type tree that represents a pointer to some type,
 *  such as we need for C.  "int *" would need a PointerType(intType) object.
 */
    public class PointerType : IType
    {

        protected IType targetType;
        public PointerType(IType targetType)
        {
            this.targetType = targetType;
        }

        public string GetName()
        {
            return ToString();
        }

        public int GetTypeIndex() { return -1; }

        public override string ToString()
        {
            return "*" + targetType;
        }
    }
}
