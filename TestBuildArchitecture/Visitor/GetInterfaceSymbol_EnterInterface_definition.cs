using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using BuildArchitecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildArchitecture.CSharpParser;

namespace TestBuildArchitecture.Visitor
{
    public class GetInterfaceSymbol_EnterInterface_definition : CSharpParserBaseVisitor<object>
    {
        public IdentifierContext IdentifierContext { get; set; }

        public override object VisitInterface_definition([NotNull] Interface_definitionContext context)
        {
            var identifier = context.identifier();
            IdentifierContext = identifier;
            return null;
        }
    }
}
