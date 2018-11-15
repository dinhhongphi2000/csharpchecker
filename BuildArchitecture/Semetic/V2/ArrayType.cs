namespace BuildArchitecture.Semetic.V2
{
    /** An element within a type type such is used in C or Java where we need to
 *  indicate the type is an array of some element type like float[] or User[].
 *  It also tracks the size as some types indicate the size of the array.
 */
    public class ArrayType : IType
    {

        protected readonly IType elemType;
        protected readonly int numElems; // some languages allow you to point at arrays of a specific size

        public ArrayType(IType elemType)
        {
            this.elemType = elemType;
            this.numElems = -1;
        }

        public ArrayType(IType elemType, int numElems)
        {
            this.elemType = elemType;
            this.numElems = numElems;
        }

        public string GetName()
        {
            return ToString();
        }

        public int GetTypeIndex() { return -1; }

        public override string ToString()
        {
            return elemType + "[]";
        }
    }
}
