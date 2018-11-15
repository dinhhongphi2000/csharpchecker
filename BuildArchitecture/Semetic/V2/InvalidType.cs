namespace BuildArchitecture.Semetic.V2
{
    public class InvalidType : IType
    {

        public string GetName()
        {
            return "INVALID";
        }

        public int GetTypeIndex() { return -1; }
    }
}
