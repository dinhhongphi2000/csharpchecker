using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildArchitecture.CSharpParser;
namespace BuildArchitecture.Rules
{
    class RecommentAddCommentBeforeFunction
    {
        [Export(typeof(Compilation_unitContext))]
        public void CheckError(ParserRuleContext context, out ErrorInformation error)
        {
            error = null;
            int a = 5;
        }
    }
}
