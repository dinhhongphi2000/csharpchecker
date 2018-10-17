using Antlr4.Runtime;
using BuildArchitecture;

namespace ModifyTokenTree
{
    class Program
    {
        static void Main(string[] args)
        {
            AntlrFileStream stream = new AntlrFileStream("sdfsd");
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
        }
    }
}
