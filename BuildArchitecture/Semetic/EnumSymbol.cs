using System.Collections.Generic;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic
{
    public class EnumSymbol : DefinitionSymbol
    {
        public EnumSymbol(string name, string fullName, string[] modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }

        public EnumSymbol(string name, string fullName, HashSet<string> modifier = null, string alias = null) 
            : base(name, fullName, modifier, alias)
        {
        }

        public static EnumSymbol GetEnumSymbol(Enum_definitionContext context, HashSet<string> modifiers, ScopedSymbolTable scopedSymbolTable)
        {
            if (context == null || scopedSymbolTable == null)
                return null;

            //Enum_definition
            //  -> Identity
            //  -> EnumBody
            //To get enum name
            var enumName = context.identifier().GetText();
            var fullName = scopedSymbolTable.Path + '.' + enumName;
            //create enum symbol
            EnumSymbol symbol = new EnumSymbol(enumName, fullName, modifiers);
            return symbol;
        }
    }
}
