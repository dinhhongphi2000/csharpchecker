using System;
using System.Collections.Generic;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic
{
    public class ClassSymbol : DefinitionSymbol
    {
        public HashSet<string> BaseTypes { get; protected set; }
        public HashSet<GenericInfo> GenericParameters { get; protected set; }

        public ClassSymbol(string name, string fullName, string[] modifier = null,
            string alias = null)
            : base(name, fullName, modifier, alias)
        {
        }

        public ClassSymbol(string name, string fullName, HashSet<string> modifier = null, string alias = null)
            : base(name, fullName, modifier, alias)
        {
        }

        /// <summary>
        /// Create ClassSymbol from Syntax node
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scopedTable"></param>
        /// <returns></returns>
        public static ClassSymbol GetClassSymbol(Type_declarationContext context, HashSet<string> modifiers, ScopedSymbolTable scopedTable)
        {
            if (context == null)
                return null;

            string symbolname = null;
            string fullName = null;
            HashSet<string> baseTypes;
            HashSet<GenericInfo> genericParameters = new HashSet<GenericInfo>();

            ///class definion ,which is node in syntax tree, contains all syntax info of class 
            var classDefinition = context.class_definition();

            //second, get name of class by call identifier
            symbolname = classDefinition.identifier().GetText();
            fullName = scopedTable.Path + "." + symbolname;

            //third, get baseType. 
            baseTypes = ClassSymbol.GetBaseType(classDefinition.class_base(), scopedTable);

            //four, get generic info. if have
            //here, we use GenericInfo instance to save declared generic of class
            //we get generic types and constrain of generic in syntax tree
            var genericTypeDeclares = ClassSymbol.GetGenericParameters(
                classDefinition.type_parameter_list()) ?? new List<string>();

            var genericConstrain =
                ClassSymbol.GetGenericParameterConstraint(
                    classDefinition.type_parameter_constraints_clauses())
                    ?? new Dictionary<string, string>();

            foreach (var item in genericTypeDeclares)
            {
                GenericInfo genericInfo = new GenericInfo();
                genericInfo.DeclareType = item;
                genericInfo.ExpectType = new HashSet<string>(genericConstrain[item].Split(','));
                genericParameters.Add(genericInfo);
            }

            ClassSymbol classSymbol = new ClassSymbol(symbolname, fullName, modifiers);
            classSymbol.BaseTypes = baseTypes;
            classSymbol.GenericParameters = genericParameters;
            return classSymbol;
        }

        private static HashSet<string> GetBaseType(Class_baseContext context, ScopedSymbolTable scopedTale)
        {
            if (context == null)
            {
                return null;
            }
            //array contains base type name of another type
            HashSet<string> baseTypeNames = new HashSet<string>();

            //Class Base node contains list of base type(class, interface)
            //Class Base node contains only CLASS TYPE node and multiple NAMESPACE OR TYPE NAME node
            //because in c#, a class inherit one class and multipe interface
            var className = context.class_type().GetText();
            if (!String.IsNullOrEmpty(className))
                baseTypeNames.Add(className);

            var interfaces = context.namespace_or_type_name();
            foreach (var item in interfaces)
            {
                baseTypeNames.Add(item.GetText());
            }

            if (baseTypeNames.Count <= 0)
                return null;
            return baseTypeNames;
        }
    }
}
