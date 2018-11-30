namespace TestBuildArchitecture.A
{
   class B
    {
        public void Add()
        {
            Test.Sub();
        }
    }

}

namespace TestBuildArchitecture
{
    class Test
    {
        public static void Sub() { }
    }
}
