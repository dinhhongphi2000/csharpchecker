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
    class GetLocalVariable : CSharpParserBaseVisitor<object>
    {
        public List<IdentifierContext> Identifiers { get; set; }

        public GetLocalVariable()
        {
            Identifiers = new List<IdentifierContext>();
        }
        public override object VisitIdentifier([NotNull] IdentifierContext context)
        {
            if(context.InRule(typeof(Local_variable_declarationContext))
                && context.InRule(typeof(Local_variable_declaratorContext)))
            {
                Identifiers.Add(context);
            }
            return base.VisitIdentifier(context);
        }
    }
}
