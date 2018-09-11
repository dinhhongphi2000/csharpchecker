using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;

namespace Caculator
{
    class Program
    {
        static void Main(string[] args)
        {
            AntlrFileStream fileStream = new AntlrFileStream("a.txt");
            CaculatorLexer lexer = new CaculatorLexer(fileStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CaculatorParser parser = new CaculatorParser(tokens);
            CaculatorParser.ProgContext progContext = parser.prog();
            EvalVisitor visitor = new EvalVisitor();
            visitor.Visit(progContext);
        }
    }
}
