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
        private const string V = @"C:\Users\ACER\Desktop\luanvan\started\TestBuildArchitecture\TestClass.cs";

        static void Main(string[] args)
        {

            WorkSpace nsg = new WorkSpace();
            nsg.InitOrUpdateParserTreeOfFile(V, GetFileContent(V));
            nsg.RunRules(V);
        }

        public static string GetFileContent(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
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
