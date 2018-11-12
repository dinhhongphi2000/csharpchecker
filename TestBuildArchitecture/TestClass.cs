namespace TestBuildArchitecture
{
    public partial class TestClass<T> where T : class
    {
        public void method(int a, string b)
        {
            for (int i = 100; i < 10; i++)
            {
                var x = 100;
            }
        }

        public T testMethodGeneric<T>(T a) where T : class, new()
        {
            return new T();
        }
    }

}
