using Antlr4.Runtime;
using BuildArchitecture;
using BuildArchitecture.Context;
using BuildArchitecture.Semetic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestBuildArchitecture
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentFile = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\TestClass.cs";

            Program program = new Program();
            WorkSpace workSpace = new WorkSpace();
            workSpace.InitOrUpdateParserTreeOfFile(currentFile, program.GetFileContent(currentFile));
            workSpace.RunRules(currentFile);
            var errorList = workSpace.GetErrors();

            //GetSolutionList();
            //GetContext();

            //TestCreateClassSymbol();
        }

        public static void TestCreateClassSymbol()
        {
            string currentFile = @"C:\Users\dinhhongphi\Desktop\luanvan\started\TestBuildArchitecture\TestClass.cs";
            StreamReader reader = new StreamReader(currentFile);
            AntlrInputStream stream = new AntlrInputStream(reader);
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);
            CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
            ScopedSymbolTable scopedSymbolTable = new ScopedSymbolTable(1, "abc");
            SemeticAnalysis semetic = new SemeticAnalysis(scopedSymbolTable);
            semetic.Visit(startContext);
        }

        public string GetFileContent(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        public static SolutionContext InitSolutionContext()
        {
            var solution = new SolutionContext(@"C:\Users\HONG PHI\source\repos\Caculator\Caculator.sln", "Caculator");
            var project = new ProjectContext(@"C:\Users\HONG PHI\source\repos\Caculator\TestBuildArchitecture\", "TestBuildArchitecture");
            solution.AddProjectNode(project.Name, project);
            return solution;
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
                    Console.Write("public IEnumerable<Lazy<Action<ParserRuleContext, ErrorInformation>>> {0} ", b);
                    Console.WriteLine("{get;set;}");
                    Console.WriteLine();
                }
            });
        }

        public static List<string> GetSolutionList()
        {
            List<string> prj = new List<string>();
            // "VisualStudio.DTE.11.0"
            // this represents your visual studio version
            // 11.0 means vs2012
            // 10.0 means vs2010
            // 9.0 means vs2005
            for (int i = 1; i < ((EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.14.0")).Solution.Projects.Count + 1; i++)
            {
                prj.Add(((EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.14.0")).Solution.Projects.Item(i).Name);
            }

            return prj;
        }
    }
}
