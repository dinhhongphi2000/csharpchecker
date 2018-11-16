using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /** For C types like "void (*)(int)", we need that to be a pointer to a function
  *  taking a single integer argument returning void.
  */
    public class FunctionType : IType
    {

        protected readonly IType returnType;
        protected readonly List<IType> argumentTypes;

        public FunctionType(IType returnType, List<IType> argumentTypes)
        {
            this.returnType = returnType;
            this.argumentTypes = argumentTypes;
        }

        public string GetName()
        {
            return ToString();
        }

        public int GetTypeIndex() { return -1; }

        public List<IType> GetArgumentTypes()
        {
            return argumentTypes;
        }

        public override string ToString()
        {
            return "*" + returnType;
        }
    }
}
