using Antlr4.Runtime.Misc;
using BuildArchitecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBuildArchitecture.Visitor
{
    public class GetIdentityContextOfFieldVisitor : CSharpParserBaseVisitor<object>
    {
        public CSharpParser.IdentifierContext[] Identifiers { get; set; }

        public override object VisitField_declaration([NotNull] CSharpParser.Field_declarationContext context)
        {
            var variableDeclaraContextList = context.variable_declarators().variable_declarator();
            Identifiers = new CSharpParser.IdentifierContext[variableDeclaraContextList.Length];
            int i = 0;
            foreach (var variableDec in variableDeclaraContextList)
            {
                var identityContext = variableDec.identifier();
                Identifiers[i++] = identityContext;
            }
            return null;
        }
    }
}
