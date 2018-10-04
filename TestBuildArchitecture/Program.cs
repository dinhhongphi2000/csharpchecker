using BuildArchitecture;
using System;
using System.Linq;

namespace TestBuildArchitecture
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();
            startup.CheckFile(@"C:\Users\HONG PHI\source\repos\Caculator\TestBuildArchitecture\TestClass.cs");
            //GetContext();
        }

        static void GetContext()
        {
            var a = typeof(BuildArchitecture.CSharpParser).Assembly.GetTypes();
            a.ToList().ForEach(e =>
            {
                if (e.FullName.Contains("BuildArchitecture.CSharpParser+"))
                {
                    var b = e.FullName.Remove(0, "BuildArchitecture.CSharpParser+".Length);
                    Console.WriteLine("{0},", b.ToUpper());
                }
            });
        }
    }
}
