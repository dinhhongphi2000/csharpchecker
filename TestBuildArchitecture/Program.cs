using BuildArchitecture;
using System;
using System.IO;
using System.Linq;

namespace TestBuildArchitecture
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentFile = @"C:\Users\HONG PHI\source\repos\Caculator\TestBuildArchitecture\TestClass.cs";
            WorkSpace workSpace = new WorkSpace();
            Program program = new Program();
            workSpace.UpdateTree(currentFile, program.GetFileContent(currentFile));
            workSpace.RunRules(currentFile);

            //GetContext();
        }

        public string GetFileContent(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        static void GetContext()
        {
            var a = typeof(BuildArchitecture.CSharpParser).Assembly.GetTypes();
            a.ToList().ForEach(e =>
            {
                if (e.FullName.Contains("BuildArchitecture.CSharpParser+"))
                {
                    
                    var b = e.FullName.Remove(0, "BuildArchitecture.CSharpParser+".Length);
                    Console.WriteLine("[ImportMany(typeof({0}))]", b);
                    Console.Write("public IEnumerable<Lazy<Action<ParserRuleContext>>> {0} ", b);
                    Console.WriteLine("{get;set;}");
                    Console.WriteLine();
                }
            });
        }
    }
}
