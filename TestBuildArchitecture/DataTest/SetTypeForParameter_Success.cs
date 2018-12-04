using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture.DataTest
{
    class SetTypeForParameter_Success
    {
        public void A(int a, ref ClassType b, StructType c) { }
    }

    class ClassType { }
    struct StructType { }
}
