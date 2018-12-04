using System;

namespace TestBuildArchitecture.DataTest
{
    class SetTypeForProperty_Success_Inheritance : Test, M
    {
        public int A { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

namespace TestBuildArchitecture
{
    public interface Test
    {
        int A
        {
            get; set;
        }
    }

    public interface M
    {
        int A { get; set; }
    }
}
