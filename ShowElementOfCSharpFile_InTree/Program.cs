using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Antlr4.Runtime.Tree;

namespace ShowElementOfCSharpFile_InTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeScope root = new TreeScope(null, "root", "root");
            TestListener listener = new TestListener(root);

            List<string> folderPaths = new List<string>();
            folderPaths.Add(@"D:\baitap\dau_tieng\QuanLySanLuong\WindowsFormsApplication6\WindowsFormsApplication6");
            while (folderPaths.Count > 0)
            {
                var directories = Directory.GetDirectories(folderPaths[0]);
                if (directories.Length > 0)
                    folderPaths.AddRange(directories);
                var files = Directory.GetFiles(folderPaths[0]);
                folderPaths.RemoveAt(0);
                files.ToList().ForEach(fp =>
                {
                    HandleFileCs(listener, fp);
                });
            }

            listener.ShowTree(root, Console.Out);

        }

        static void HandleFileCs(TestListener listener, string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Extension == ".cs")
            {
                AntlrFileStream stream = new AntlrFileStream(filePath);
                CSharpLexer lexer = new CSharpLexer(stream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CSharpParser parser = new CSharpParser(tokens);
                //parser.RemoveErrorListeners();
                //parser.AddErrorListener(new CustomError());
                CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
                ParseTreeWalker walker = new ParseTreeWalker();
                walker.Walk(listener, startContext);
            }
        }
    }
}
