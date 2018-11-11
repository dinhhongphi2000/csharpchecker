using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture
{
    public partial class TestClass<T> where T : class
    {
        public void method()
        {
            for(int i = 100; i < 10; i++)
            {
                var x = 100;
            }
        }
    }
}
