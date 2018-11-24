using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using BuildArchitecture;
using static BuildArchitecture.CSharpParser;

namespace TestBuildArchitecture.Visitor
{
    class Qualified_identifierVisitor : CSharpParserBaseVisitor<object>
    {
        public ParserRuleContext NameSpaceNode { get; set; }

        public override object VisitQualified_identifier([NotNull] CSharpParser.Qualified_identifierContext context)
        {
            if (context.InRule(typeof(NamespaceContext)))
            {
                NameSpaceNode = context;
                return null;
            }
            else
                return base.VisitQualified_identifier(context);
        }
    }
}
