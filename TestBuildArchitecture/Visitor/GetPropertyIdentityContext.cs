using Antlr4.Runtime.Misc;
using BuildArchitecture;

namespace TestBuildArchitecture.Visitor
{
    public class GetPropertyIdentityContext : CSharpParserBaseVisitor<object>
    {
        /// <summary>
        /// Last identity in identityList
        /// </summary>
        public CSharpParser.IdentifierContext IdentityContext { get; set; }

        public override object VisitProperty_declaration([NotNull] CSharpParser.Property_declarationContext context)
        {
            var propertyIdentityContext = context.member_name().namespace_or_type_name().identifier();
            int identityCount = propertyIdentityContext.Length;
            IdentityContext = propertyIdentityContext[identityCount - 1]; //property name of class, struct
            return null;
        }

    }
}
