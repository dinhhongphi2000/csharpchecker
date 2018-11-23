using Antlr4.Runtime.Misc;
using BuildArchitecture;

namespace TestBuildArchitecture.Visitor
{
    public class GetClass_definitionVisitor : CSharpParserBaseVisitor<object>
    {
        public CSharpParser.IdentifierContext ClassIdentity { get; set; }
        public override object VisitClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            ClassIdentity = context.identifier();
            return null;
        }
    }
}
