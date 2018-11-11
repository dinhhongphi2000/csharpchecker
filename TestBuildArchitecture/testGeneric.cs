using System;

namespace BuildArchitecture.Semetic
{
    public class Math
    {
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

        public int Plus<T>(T a, T b) where T: class, new()
        {
            return 1;
        }
    }
}
