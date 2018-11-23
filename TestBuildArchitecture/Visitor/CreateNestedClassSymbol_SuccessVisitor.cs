using Antlr4.Runtime.Misc;
using BuildArchitecture;

namespace TestBuildArchitecture.Visitor
{
    public class CreateNestedClassSymbol_SuccessVisitor : CSharpParserBaseVisitor<object>
    {
        public CSharpParser.IdentifierContext ClassIdentity { get; set; }

        public override object VisitClass_definition([NotNull] CSharpParser.Class_definitionContext context)
        {
            var classIdentity = context.identifier();
            if (classIdentity.GetText() == "NestedClass")
            {
                ClassIdentity = classIdentity;
                return null;
            }
            return base.VisitClass_definition(context);
        }
    }
}
