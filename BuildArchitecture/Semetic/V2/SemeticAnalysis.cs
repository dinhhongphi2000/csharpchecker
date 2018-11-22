using Antlr4.Runtime.Misc;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic.V2
{
    public class SemeticAnalysis : CSharpParserBaseListener
    {
        private IScope currentScope;

        public override void EnterCompilation_unit([NotNull] Compilation_unitContext context)
        {
            GlobalScope global = new GlobalScope(null);
            currentScope = global;
        }

        public override void ExitCompilation_unit([NotNull] Compilation_unitContext context)
        {
            currentScope = currentScope.GetEnclosingScope();
        }

        /// <summary>
        /// Define namespace scope
        /// </summary>
        /// <param name="context"></param>
        public override void EnterQualified_identifier([NotNull] CSharpParser.Qualified_identifierContext context)
        {
            //create scope namespace
            if (context.InRule(typeof(NamespaceContext)))
            {
                var identityName = context.identifier();
                NamespaceSymbol symbolScope = null;
                foreach (var name in identityName)
                {
                    symbolScope = new NamespaceSymbol(name.GetText());
                    symbolScope.SetEnclosingScope(currentScope);
                    currentScope.Define(symbolScope);
                    currentScope = symbolScope;
                }
                context.Scope = symbolScope;

            }
        }

        public override void ExitQualified_identifier([NotNull] Qualified_identifierContext context)
        {
            if (context.InRule(typeof(NamespaceContext)))
            {
                var identityName = context.identifier();
                for (int i = 1; i <= identityName.Length; i++)
                    currentScope = currentScope.GetEnclosingScope();
            }
        }

        /// <summary>
        /// Define class scope and symbol
        /// </summary>
        /// <param name="context"></param>
        public override void EnterClass_definition([NotNull] Class_definitionContext context)
        {
            var classNameContext = context.identifier();
            ClassSymbol classSymbol = new ClassSymbol(classNameContext.GetText());
            classSymbol.SetEnclosingScope(currentScope);
            classSymbol.DefNode = context;

            classNameContext.Symbol = classSymbol;
            classNameContext.Scope = classSymbol;

            currentScope.Define(classSymbol);
            currentScope = classSymbol;
        }

        public override void ExitClass_definition([NotNull] Class_definitionContext context)
        {
            currentScope = currentScope.GetEnclosingScope();
        }

        /// <summary>
        /// Define struct scope and symbol
        /// </summary>
        /// <param name="context"></param>
        public override void EnterStruct_definition([NotNull] Struct_definitionContext context)
        {
            var structIdentityContext = context.identifier();
            StructSymbol structSymbol = new StructSymbol(structIdentityContext.GetText());
            structSymbol.SetEnclosingScope(currentScope);
            structSymbol.DefNode = context;

            structIdentityContext.Symbol = structSymbol;
            structIdentityContext.Scope = structSymbol;

            currentScope.Define(structSymbol);
            currentScope = structSymbol;
        }

        public override void ExitStruct_definition([NotNull] Struct_definitionContext context)
        {
            currentScope = currentScope.GetEnclosingScope();
        }

        /// <summary>
        /// Define field, property, of class, struct
        /// </summary>
        /// <param name="context"></param>
        public override void EnterProperty_declaration([NotNull] Property_declarationContext context)
        {
            var propertyIdentityContext = context.member_name().namespace_or_type_name().identifier();
            int identityCount = propertyIdentityContext.Length;
            var lastedIdentityContext = propertyIdentityContext[identityCount - 1]; //property name of class, struct

            FieldSymbol fieldSymbol = new FieldSymbol(lastedIdentityContext.GetText())
            {
                DefNode = lastedIdentityContext
            };

            lastedIdentityContext.Symbol = fieldSymbol;
            lastedIdentityContext.Scope = currentScope;

            currentScope.Define(fieldSymbol);
        }

        public override void EnterField_declaration([NotNull] Field_declarationContext context)
        {
            var variableDeclaraContextList = context.variable_declarators().variable_declarator();
            foreach(var variableDec in variableDeclaraContextList)
            {
                var identityContext = variableDec.identifier();
                FieldSymbol fieldSymbol = new FieldSymbol(identityContext.GetText())
                {
                    DefNode = identityContext
                };

                identityContext.Symbol = fieldSymbol;
                identityContext.Scope = currentScope;

                currentScope.Define(fieldSymbol);
            }
        }

        /// <summary>
        /// Define function symbol and scope
        /// </summary>
        /// <param name="context"></param>
        public override void EnterMethod_member_name([NotNull] Method_member_nameContext context)
        {
            var identityContextList = context.identifier();
            var lastestIdentityContext = identityContextList[identityContextList.Length - 1]; //is name of method

            MethodSymbol methodSymbol = new MethodSymbol(lastestIdentityContext.GetText())
            {
                DefNode = lastestIdentityContext
            };
            methodSymbol.SetEnclosingScope(currentScope);

            lastestIdentityContext.Symbol = methodSymbol;
            lastestIdentityContext.Scope = methodSymbol;

            currentScope.Define(methodSymbol);
            currentScope = methodSymbol;
        }

        public override void ExitMethod_member_name([NotNull] Method_member_nameContext context)
        {
            currentScope = currentScope.GetEnclosingScope();
        }

        /// <summary>
        /// Define parameter of function
        /// </summary>
        /// <param name="context"></param>
        public override void EnterArg_declaration([NotNull] Arg_declarationContext context)
        {
            var identityContext = context.identifier();
            ParameterSymbol parameterSymbol = new ParameterSymbol(identityContext.GetText())
            {
                DefNode = identityContext
            };

            identityContext.Symbol = parameterSymbol;
            identityContext.Scope = currentScope;

            currentScope.Define(parameterSymbol);
        }

        /// <summary>
        /// Define local scope
        /// </summary>
        /// <param name="context"></param>
        public override void EnterBlock([NotNull] BlockContext context)
        {
            LocalScope scope = new LocalScope(currentScope);
            context.Scope = scope;
            currentScope = scope;
        }

        public override void ExitBlock([NotNull] BlockContext context)
        {
            currentScope = currentScope.GetEnclosingScope();
        }



    }
}
