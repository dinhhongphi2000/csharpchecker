using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace CSharpChecker.LightBulb
{
    [Export(typeof(TestImport))]
    class TestImport
    {
        public string Test = "Test complete" ;

        public TestImport(string test)
        {
            Test = test;
        }
    }
}
