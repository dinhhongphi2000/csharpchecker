using Antlr4.Runtime.Misc;
using System.Collections.Generic;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic
{
    public class ClassSymbol : DefinitionSymbol
    {
        public List<DefinitionSymbol> BaseTypes { get; set; }
        public List<GenericInfo> GenericParameters { get; set; }

        public ClassSymbol(string name, string fullName, string[] modifier = null, 
            string alias = null)
            : base(name, fullName, modifier, alias)
        {
        }

        public ClassSymbol(string name, string fullName, List<string> modifier = null, string alias = null)
            : base(name, fullName, modifier, alias)
        {
        }

        public static Symbol Create([NotNull]Type_declarationContext context, ScopedSymbolTable scopedTable)
        {
            List<string> modifiers = new List<string>();
            string symbolname = null;
            string fullName = null;
            List<DefinitionSymbol> baseTypes = new List<DefinitionSymbol>();
            List<GenericInfo> genericParameters = new List<GenericInfo>();

            var modifierContexts = context.all_member_modifiers()?.all_member_modifier();
            foreach (var modifierCon in modifierContexts)
            {
                modifiers.Add(modifierCon.GetText());
            }
            var classdefinition = context.class_definition();
            symbolname = classdefinition.identifier().GetText();
            fullName = scopedTable.Path + "." + symbolname;
            ClassSymbol classSymbol = new ClassSymbol(symbolname,fullName, modifiers);
            return classSymbol;
        }

        private static List<DefinitionSymbol> GetBaseType([NotNull] Class_baseContext context, ScopedSymbolTable scopedTale)
        {
            var class_typeContext = context.class_type();
            if(class_typeContext != null)
            {
                var base_types = class_typeContext.namespace_or_type_name();
                foreach(var typeName in base_types)
                {

                }
            }
        }
    }
}
