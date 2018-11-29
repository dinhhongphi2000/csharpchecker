using Antlr4.Runtime.Misc;
using BuildArchitecture;
using System.Collections.Generic;

namespace TestBuildArchitecture.Visitor
{
    public class GetVariableIdentityLocalVisitor : CSharpParserBaseVisitor<object>
    {
        public List<CSharpParser.IdentifierContext> IdentitiesContext { get; set; }

        public GetVariableIdentityLocalVisitor()
        {
            IdentitiesContext = new List<CSharpParser.IdentifierContext>();
        }

        public override object VisitLocal_variable_declarator([NotNull] CSharpParser.Local_variable_declaratorContext context)
        {
            var identifier = context.identifier();
            IdentitiesContext.Add(identifier);
            return null;
        }
    }
}
