using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildArchitecture;

namespace TestBuildArchitecture
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();
            startup.CheckFile(@"C:\Users\HONG PHI\source\repos\Caculator\BuildArchitecture\Provider.cs");
        }
    }
}
