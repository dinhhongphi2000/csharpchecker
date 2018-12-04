using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture.DataTest
{
    class SetTypeForFunction_SelfDefinitionType_Success
    {
        public B Test()
        {
            return new B();
        }
    }

    class B
    {

    }
}
