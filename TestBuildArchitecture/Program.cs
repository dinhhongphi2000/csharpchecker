﻿using Antlr4.Runtime;
using BuildArchitecture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestBuildArchitecture
{
    class Program
    {
        private const string V = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\TestClass.cs";
        private bool _a = true;
        private string[] _asdf = new string[5];
        static void Main(string[] args)
        {

            WorkSpace nsg = WorkSpace.Instance;
            nsg.InitOrUpdateParserTreeOfFile(V, GetFileContent(V));
            nsg.RunRules(V);
            try
            {

            }
            catch (Exception e)
            {
            }
        }

        public static string GetFileContent(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
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
