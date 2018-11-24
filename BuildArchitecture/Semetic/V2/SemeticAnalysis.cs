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
        public override void EnterNamespace([NotNull] NamespaceContext context)
        {
            //create scope namespace
            var identityList = context.qualified_identifier().identifier();
            NamespaceSymbol symbolScope = null;
            foreach (var identity in identityList)
            {
                symbolScope = new NamespaceSymbol(identity.GetText());
                symbolScope.SetEnclosingScope(currentScope);
                currentScope.Define(symbolScope);

                identity.Scope = symbolScope;
                identity.Symbol = symbolScope;

                currentScope = symbolScope;
            }
        }

        public override void ExitNamespace([NotNull] NamespaceContext context)
        {
            var identityName = context.qualified_identifier().identifier();
            for (int i = 1; i <= identityName.Length; i++)
                currentScope = currentScope.GetEnclosingScope();
        }

        /// <summary>
        /// Define class scope and symbol
        /// </summary>
        /// <param name="context"></param>
        public override void EnterClass_definition([NotNull] Class_definitionContext context)
        {
            var classIdentityContext = context.identifier();
            ClassSymbol classSymbol = new ClassSymbol(classIdentityContext.GetText());
            classSymbol.SetEnclosingScope(currentScope);
            classSymbol.DefNode = context;

            classIdentityContext.Symbol = classSymbol;
            classIdentityContext.Scope = classSymbol;

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
            structSymbol.DefNode = structIdentityContext;

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
            //same as : int a, b = 10
            foreach (var variableDec in variableDeclaraContextList)
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
        public override void EnterMethod_declaration([NotNull] Method_declarationContext context)
        {
            var identityContextList = context.method_member_name().identifier();
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

        public override void ExitMethod_declaration([NotNull] Method_declarationContext context)
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
            currentScope = scope;
        }

        public override void ExitBlock([NotNull] BlockContext context)
        {
            currentScope = currentScope.GetEnclosingScope();
        }



    }
}
