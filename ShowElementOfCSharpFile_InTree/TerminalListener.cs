using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace ShowElementOfCSharpFile_InTree
{
    public class TerminalListener : CSharpParserBaseListener
    {
        //dinh hong phi
        public override void VisitTerminal([NotNull] ITerminalNode node)
        {
            base.VisitTerminal(node);
            Console.WriteLine(node.GetText());
        }

        
    }
}
