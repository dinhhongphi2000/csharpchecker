using System.Collections.Generic;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic
{
    public class FuncSymbol : DeclarationSymbol
    {
        public HashSet<string> ParameterTypes { get; protected set; }
        public HashSet<GenericInfo> GenericParameters { get; protected set; }

        public FuncSymbol(string name, string[] modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public FuncSymbol(string name, HashSet<string> modifier = null, string alias = null) 
            : base(name, modifier, alias)
        {
        }

        public static FuncSymbol GetFuncSymbol(Class_member_declarationContext context)
        {
            if (context == null)
            {
                return null;
            }
            
            //methodMember = null => this method is constructor
            string returnType = null;
            string functionName = null;
            HashSet<string> parameterTypes = new HashSet<string>();
            HashSet<GenericInfo> genericInfos = new HashSet<GenericInfo>();
            
            //Class_member_declaration includes All_member_modifiers and Common_member_declaration
            //get modifier form All_member_modifiers
            var modifiers = FuncSymbol.GetAllMemberModifiers(context.all_member_modifiers());

            //Common_member_declaration includes Typed_member_declaration OR Constructor_declaration
            //OR Method_declaration
            //Typed_member_declaration contains return type of method
            //Constructor_declaration contains constructor name of class, parameters of function
            //Method_declaration contains method name, parameters, generic type
            Method_declarationContext methodDeclaration = null;
            Constructor_declarationContext constructorDeclaration = null;
            var memberDeclaration = context.common_member_declaration();
            var memberTypeName = memberDeclaration.GetType().Name;
            switch (memberTypeName)
            {
                case "Typed_member_declarationContext":
                    //get return type from Typed_member_declaration
                    var typedMember = memberDeclaration.typed_member_declaration();
                    returnType = typedMember.type().GetText();
                    //get method declaration
                    methodDeclaration = typedMember.method_declaration();
                    break;
                case "Constructor_declarationContext":
                    constructorDeclaration = memberDeclaration.constructor_declaration();
                    functionName = constructorDeclaration.identifier().GetText();
                    break;
                case "Method_declarationContext":
                    returnType = "void";
                    methodDeclaration = memberDeclaration.method_declaration();
                    break;
            }
            if (methodDeclaration != null)
            {
                //get name and generics type from Method_declaration
                functionName = methodDeclaration.method_member_name().GetText();
                //get generics type
                var genericParameters = FuncSymbol.GetGenericParameters(methodDeclaration.type_parameter_list());

                var genericConstrain = FuncSymbol.GetGenericParameterConstraint(
                    methodDeclaration.type_parameter_constraints_clauses());

                if(genericParameters != null)
                {
                   foreach(var item in genericParameters)
                    {
                        GenericInfo genericInfo = new GenericInfo();
                        genericInfo.DeclareType = item;

                        genericInfo.ExpectType = genericConstrain != null 
                            ? new HashSet<string>(genericConstrain[item].Split(',')) : null;

                        genericInfos.Add(genericInfo);
                    }
                }
            }
            //get parameters
            var formalParameterList = methodDeclaration.formal_parameter_list();
            if(formalParameterList != null)
            {
                foreach(var item in formalParameterList.fixed_parameters().fixed_parameter())
                {
                    parameterTypes.Add(item.arg_declaration().type().GetText());
                }
            }

            //create FuncSymbol
            FuncSymbol symbol = new FuncSymbol(functionName, modifiers);
            symbol.Type = returnType;
            symbol.ParameterTypes = parameterTypes.Count > 0 ? parameterTypes : null;
            symbol.GenericParameters = genericInfos.Count > 0 ? genericInfos : null;
            return symbol;
        }
    }
}
