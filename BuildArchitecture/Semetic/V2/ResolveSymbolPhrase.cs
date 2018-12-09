using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic.V2
{
    public class ResolveSymbolPhrase : CSharpParserBaseVisitor<object>
    {
        private IScope currentScope;
        private List<ErrorInformation> errorTable;

        public void SetErrorTable(List<ErrorInformation> errorTable)
        {
            this.errorTable = errorTable ?? throw new ArgumentNullException();
        }

        public override object VisitCompilation_unit([NotNull] Compilation_unitContext context)
        {
            currentScope = context.Scope;
            return base.VisitCompilation_unit(context);
        }

        public override object VisitIdentifier([NotNull] IdentifierContext context)
        {
            if (context.Scope != null)
                currentScope = context.Scope;
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
                var symbol = scope.ResolveType(item.GetText());
                if (symbol != null)
                {
                    item.Symbol = symbol;
                    item.Scope = (IScope)symbol;
                }
                else
                {
                    //symbol don't be declare. Error
                    //AddError(new ErrorInformation());
                }
            }
            return identifierContexts[identifierContexts.Length - 1];
        }

        public override object VisitClass_base([NotNull] Class_baseContext context)
        {
            return base.VisitClass_base(context);
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
            var type = ResolveType(context.type(), currentScope);

            if (context.field_declaration() != null)
            {
                return SetTypeForField(context.field_declaration(), type);
            }
            else if (context.method_declaration() != null)
            {
                SetTypeForFunction(context.method_declaration(), type);
            }
            else if (context.property_declaration() != null)
            {
                return SetTypeForProperty(context.property_declaration(), type);
            }
            return null;

        }


        /// <summary>
        /// Set type for parameter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitArg_declaration([NotNull] Arg_declarationContext context)
        {
            var type = ResolveType(context.type(), currentScope);
            var argSymbol = context.identifier().Symbol as ParameterSymbol;
            argSymbol.SetType(type);
            return context.identifier();
        }
        public override object VisitLocal_variable_declaration([NotNull] Local_variable_declarationContext context)
        {
            var variableContexts = context.local_variable_declarator();
            List<VariableSymbol> symbols = new List<VariableSymbol>();
            foreach (var item in variableContexts)
            {
                var identifier = (IdentifierContext)item.identifier();
                symbols.Add((VariableSymbol)identifier.Symbol);
            }
            if (context.local_variable_type().VAR() != null)
            {

            }
            else if (context.local_variable_type().type() != null)
            {
                IScope scope = symbols[0].GetScope();
                var typeContext = context.local_variable_type().type();
                IType type = ResolveType(typeContext, scope);
                foreach (var item in symbols)
                {
                    item.SetType(type);
                }
            }
            return null;
        }

        #region self definition function
        /// <summary>
        /// Get Symbol type of typeContext
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private IType ResolveType(TypeContext context, IScope scope)
        {
            if (scope == null) return null;
            IType type;
            if (context.TypeName != null)
            {
                type = (IType)scope.ResolveType(context.TypeName);
                if (type == null)
                {
                    //symbol don't be declare. Error
                    //AddError(new ErrorInformation());
                }
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

        private List<IdentifierContext> SetTypeForField(Field_declarationContext context, IType type)
        {
            var fields = (List<IdentifierContext>)Visit(context);

            foreach (var item in fields)
            {
                if (item == null)
                    continue;
                var field = (FieldSymbol)item.Symbol;
                field.SetType(type);
            }
            return fields;
        }

        private List<IdentifierContext> SetTypeForProperty(Property_declarationContext context, IType type)
        {
            //set type for property
            //Example Property: int A.B.C {get; set;}
            var propertyIdentityContext = context.member_name().namespace_or_type_name().identifier();

            //resolve A find B, resolve B, find C. 
            IScope parentScope = currentScope;
            for (int i = 0; i < propertyIdentityContext.Length - 1; i++)
            {
                var item = propertyIdentityContext[i];
                ISymbol tempScope = parentScope.ResolveType(item.GetText());
                if (tempScope != null)
                {
                    item.Symbol = tempScope;
                    item.Scope = (IScope)tempScope;
                    parentScope = (IScope)tempScope;
                }
                else
                {
                    //error cannot find symbol in parentScope
                    //AddError(new ErrorInformation());
                    return null;
                }
            }
            //check c in A.B
            var symbol = parentScope.Resolve(propertyIdentityContext.Last().GetText());
            if (symbol != null)
            {
                //symbol C in A.B then set type for C
                (symbol as FieldSymbol).SetType(type);
                return new List<IdentifierContext>(new IdentifierContext[] { propertyIdentityContext.Last() });
            }
            else
            {
                //error, cannot find symbol in parentScope
                //AddError(new ErrorInformation());
                return null;
            }
        }

        private List<IdentifierContext> SetTypeForFunction(Method_declarationContext context, IType type)
        {
            var identifierContext = (IdentifierContext)Visit(context.method_member_name());
            var symbol = identifierContext.Symbol as FunctionSymbol;
            symbol.SetType(type);
            return new List<IdentifierContext>(new IdentifierContext[] { identifierContext });
        }

        private void AddError(ErrorInformation error)
        {
            if (errorTable != null)
                errorTable.Add(error);
        }

        public List<ErrorInformation> GetErrors()
        {
            return errorTable;
        }
        #endregion
    }
}
