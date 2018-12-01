using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic.V2
{
    public class ResolveSymbolPhrase : CSharpParserBaseVisitor<object>
    {
        private IScope currentScope;

        public override object VisitCompilation_unit([NotNull] Compilation_unitContext context)
        {
            currentScope = context.Scope;
            return base.VisitCompilation_unit(context);
        }

        public override object VisitIdentifier([NotNull] CSharpParser.IdentifierContext context)
        {
            if (context.Scope != null)
                currentScope = context.Scope;
            return context;
        }

        #region set symbol and scope for type ref in expresion (EX: A.B in A.B a = new A.B())
        public override object VisitNamespace_or_type_name([NotNull] Namespace_or_type_nameContext context)
        {
            var identifierContexts = context.identifier();
            var scope = currentScope;
            foreach (var item in identifierContexts)
            {
                var symbol = scope.Resolve(item.GetText());
                if (symbol != null)
                {
                    item.Symbol = symbol;
                    scope = (IScope)symbol;
                }
                else
                {
                    //symbol don't be declare. Error
                }
            }
            return identifierContexts[identifierContexts.Length - 1];
        }
        #endregion

        #region Set type for field
        public override object VisitVariable_declarators([NotNull] Variable_declaratorsContext context)
        {
            List<IdentifierContext> list = new List<IdentifierContext>();
            var listContext = context.variable_declarator();
            foreach (var item in listContext)
            {
                list.Add((IdentifierContext)Visit(item));
            }
            return list;
        }

        public override object VisitTyped_member_declaration([NotNull] Typed_member_declarationContext context)
        {
            var typeContext = (IdentifierContext)Visit(context.type());

            if (context.field_declaration() != null)
            {
                var fields = (List<IdentifierContext>)Visit(context.field_declaration());

                foreach (var item in fields)
                {
                    var field = (FieldSymbol)item.Symbol;
                    field.SetType((IType)typeContext.Symbol);
                }
                return fields;
            }
            else if (context.method_declaration() != null)
            {
            }
            return null;

        }
        #endregion

        public override object VisitChildren([NotNull] IRuleNode node)
        {
            object result = null;
            int n = node.ChildCount;
            for (int i = 0; i < n; i++)
            {
                if (!ShouldVisitNextChild(node, result))
                {
                    break;
                }
                IParseTree c = node.GetChild(i);
                if (c is TerminalNodeImpl || c is ErrorNodeImpl)
                    continue;
                object childResult = c.Accept(this);
                result = AggregateResult(result, childResult);
            }
            return result;
        }
    }
}
