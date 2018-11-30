using Antlr4.Runtime.Misc;
using BuildArchitecture;
using BuildArchitecture.Semetic.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildArchitecture.CSharpParser;

namespace TestBuildArchitecture.Visitor
{
    public class ResolveSymbolDeclareOnOtherFile : CSharpParserBaseVisitor<object>
    {
        public ISymbol Symbol { get; set; }
        private IdentifierContext typeContext;

        public override object VisitIdentifier([NotNull] IdentifierContext context)
        {
            if (context.InRule(typeof(Typed_member_declarationContext)))
            {
                if (context.InRule(typeof(TypeContext)))
                {
                    typeContext = context;
                }
                else if(context.InRule(typeof(Member_nameContext)))
                {
                    Symbol = context.Scope.Resolve(typeContext.GetText());
                    return null;
                }
            }
            return base.VisitIdentifier(context);
        }
    }
}
