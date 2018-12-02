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

        public override object VisitIdentifier([NotNull] IdentifierContext context)
        {
            return context;
        }

        /// <summary>
        /// set symbol and scope for type ref in expresion (EX: A.B in A.B a = new A.B())
        /// </summary>
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

        /// <summary>
        /// int a,b,c. Get List IdentifierContext of a,b,c
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Set type for member of class or struct that have type. (functions, fields , properties)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitTyped_member_declaration([NotNull] Typed_member_declarationContext context)
        {
            if (context.field_declaration() != null)
            {
                var fields = (List<IdentifierContext>)Visit(context.field_declaration());
                var type = ResolveType(context.type(), fields[0].Scope);
                foreach (var item in fields)
                {
                    var field = (FieldSymbol)item.Symbol;
                    field.SetType(type);
                }
                return fields;
            }
            else if (context.method_declaration() != null)
            {
            }
            return null;

        }

        public override object VisitLocal_variable_declaration([NotNull] Local_variable_declarationContext context)
        {
            var variableContexts = context.local_variable_declarator();
            List<VariableSymbol> symbols = new List<VariableSymbol>();
            foreach (var item in variableContexts)
            {
                var identifier = (IdentifierContext)Visit(item);
                symbols.Add((VariableSymbol)identifier.Symbol);
            }

            if (context.local_variable_type().VAR() != null)
            {

            }else if (context.local_variable_type().type() != null)
            {
                IScope scope = symbols[0].GetScope();
                var typeContext = context.local_variable_type().type();
                IType type = ResolveType(typeContext, scope);
                foreach (var item in variableContexts)
                {
                    var variable = (IdentifierContext)Visit(item);
                    var symbol = variable.Symbol as VariableSymbol;
                    symbol.SetType(type);
                }
            }
            return null;
        }

        /// <summary>
        /// Get Symbol type of typeContext
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private IType ResolveType(TypeContext context, IScope scope)
        {
            IType type;
            if (context.TypeName != null)
            {
                type = (IType)scope.Resolve(context.TypeName);
            }
            else
            {
                var classContext = (IdentifierContext)Visit(context);
                type = (IType)classContext.Symbol;
            }
            return type;
        }

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
