using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime.Tree;

namespace AnalysticCSharpFile
{
    class Program
    {
        static void Main(string[] args)
        {
            TestVisitor visitor = new TestVisitor();
            List<string> folderPaths = new List<string>();
            folderPaths.Add(@"C:\Users\HONG PHI\source\repos\Caculator\AnalysticCSharpFile\bin\Debug\build");
            //folderPaths.Add(@"D:\baitap\dau_tieng\QuanLySanLuong\WindowsFormsApplication6\WindowsFormsApplication6");
            while (folderPaths.Count > 0)
            {
                var directories = Directory.GetDirectories(folderPaths[0]);
                if (directories.Length > 0)
                    folderPaths.AddRange(directories);
                var files = Directory.GetFiles(folderPaths[0]);
                folderPaths.RemoveAt(0);
                files.ToList().ForEach(fp =>
                {
                    HandleFileCs(visitor, fp);
                });
            }

            //visitor.ListNameSpace(Console.Out);
            visitor.ListMethodName(Console.Out);
        }

        static void HandleFileCs(TestVisitor visitor, string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Extension == ".cs")
            {
                AntlrFileStream stream = new AntlrFileStream(filePath);
                CSharpLexer lexer = new CSharpLexer(stream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CSharpParser parser = new CSharpParser(tokens);
                CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
                TestListener listener = new TestListener(parser);
                IParseTree tree = parser.compilation_unit();
                ParseTreeWalker walker = new ParseTreeWalker();
                walker.Walk(listener, startContext);
                StringBuilder streamwritter = new StringBuilder(stream.ToString());
                foreach (Tuple<int, string> tup in listener.GetTuples())
                {
                    streamwritter.Remove(tup.Item1, tup.Item2.Length).Insert(tup.Item1, tup.Item2);
                }
                //visitor.Visit(startContext);
                StreamWriter writer = new StreamWriter(filePath);
                writer.Write(streamwritter);
                writer.Dispose();
            }
        }
    }
}
