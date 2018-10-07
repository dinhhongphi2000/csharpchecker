using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Linq;
using System.Reflection;

namespace BuildArchitecture
{
    public class WorkSpace
    {
        public void CheckFile(string path)
        {
            AntlrFileStream stream = new AntlrFileStream(path);
            CSharpLexer lexer = new CSharpLexer(stream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);
            CSharpParser.Compilation_unitContext startContext = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();

            walker.Walk(NodeVisitedListener.Instance, startContext);
        }
    }
}
