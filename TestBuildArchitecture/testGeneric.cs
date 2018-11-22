namespace BuildArchitecture.Semetic
{
    class Test
    {
        public int score { get; set; }
        protected float number, c = 5;
        public Person Person;

        public int Add(int a, int b)
        {
            int c;
            int d = a + b;
            StructTest x = new StructTest();
            return d;
        }
    }

    class Sum : Cong1, Cong2
    {
        void Cong1.Enter() { }
        void Cong2.Enter() { }
    }

    interface Cong1
    {
        void Enter();
    }

    interface Cong2
    {
        void Enter();
    }

    struct StructTest
    {
        int b;

        public void Test(int a, int b)
        {

        }
    }

    class Person { }
}

namespace BuildArchitecture
{
}
