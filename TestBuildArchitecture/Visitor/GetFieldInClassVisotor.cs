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
    public class GetFieldInClassVisotor : CSharpParserBaseVisitor<object>
    {
        public IdentifierContext Identifier { get; set; }

        public override object VisitIdentifier([NotNull] IdentifierContext context)
        {
            if (context.InRule(typeof(Field_declarationContext)))
            {
                Identifier = context;
            }
            return null;
        }
    }
}
