using Antlr4.Runtime.Misc;
using BuildArchitecture;
using System;

namespace TestBuildArchitecture.Visitor
{
    public class GetFunctionIdentityVisitor : CSharpParserBaseVisitor<object>
    {
        public CSharpParser.IdentifierContext FunctionIdentifier { get; set; }

        public override object VisitMethod_declaration([NotNull] CSharpParser.Method_declarationContext context)
        {
            var identityContextList = context.method_member_name().identifier();
            FunctionIdentifier = identityContextList[identityContextList.Length - 1]; //is name of method
            return null;
        }
    }
}
