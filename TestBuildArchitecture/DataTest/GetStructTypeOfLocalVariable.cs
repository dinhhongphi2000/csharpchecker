using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture.DataTest
{
    public struct Math
    {
        int a;
        int b;
    }
    class GetStructTypeOfLocalVariable
    {
        public void Hello()
        {
            Math math;
        }
    }
}
