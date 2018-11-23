using Antlr4.Runtime.Misc;
using BuildArchitecture;

namespace TestBuildArchitecture.Visitor
{
    class GetStructIdentityVisitor : CSharpParserBaseVisitor<object>
    {
        public CSharpParser.IdentifierContext StructIdentity { get; set; }

        public override object VisitStruct_definition([NotNull] CSharpParser.Struct_definitionContext context)
        {
            StructIdentity = context.identifier();
            return null;
        }
    }
}
