namespace BuildArchitecture.Semetic
{
    public class Math
    {
        int a;
        public Math math { get; set; }
        public Math math2 = new Math();
        public Math()
        {

        }
        public Math(int type)
        {

        }

        public static int Plus(int a, int b)
        {
            return 1;
        }

        public float Plus<T>(T a, T b) where T : class, new()
        {
            return "";
        }

        public string Plus<T>(T a, T b) where T : class, new()
        {
            return "";
        }

        public void sub()
        {

        }

        class B
        {

        }

        public struct C
        {

        }

        class D<M>
        {
            public D() { }
        }

    }
}
