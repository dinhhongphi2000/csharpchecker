using Antlr4.Runtime.Misc;
using BuildArchitecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture.Visitor
{
    public class GetFunctionParameterVisitor : CSharpParserBaseVisitor<object>
    {
        public List<CSharpParser.IdentifierContext> Parameters { get; set; }

        public GetFunctionParameterVisitor()
        {
            Parameters = new List<CSharpParser.IdentifierContext>();
        }

        public override object VisitArg_declaration([NotNull] CSharpParser.Arg_declarationContext context)
        {
            Parameters.Add(context.identifier());
            return null;
        }
    }
}
