using System;

namespace BuildArchitecture.Semetic.V2
{
    /** A "typedef int I;" in C results in a TypeAlias("I", ptrToIntegerType) */
    public class TypeAlias : BaseSymbol, IType
    {

        protected Type targetType;
        public TypeAlias(String name, Type targetType) : base(name)
        {
            this.targetType = targetType;
        }

        public int GetTypeIndex() { return -1; }

        public Type GetTargetType()
        {
            return targetType;
        }
    }
}
