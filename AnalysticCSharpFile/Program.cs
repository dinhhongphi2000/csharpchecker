using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysticCSharpFile
{
    class Program
    {
        static void Main(string[] args)
        {
            AntlrFileStream stream = new AntlrFileStream(@"C:\Users\HONG PHI\source\repos\Caculator\AnalysticCSharpFile\bin\Debug\a.cs");
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);
            CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
            TestVisitor visitor = new TestVisitor();
            visitor.Visit(startContext);
        }
    }
}
