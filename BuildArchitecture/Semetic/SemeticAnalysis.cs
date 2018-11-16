using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture.Semetic
{
    public class SemeticAnalysis : CSharpParserBaseVisitor<object>
    {
        private ScopedSymbolTable _current;

        public SemeticAnalysis(ScopedSymbolTable scopedSymbolTable)
        {
            _current = scopedSymbolTable ?? throw new ArgumentNullException();
        }

        public override object VisitType_declaration([NotNull] CSharpParser.Type_declarationContext context)
        {
            var modifiers = this.GetAllMemberModifiers(context.all_member_modifiers());
            if (context.class_definition() != null)
            {
                var classSymbol = ClassSymbol.GetClassSymbol(context.class_definition(), modifiers, _current);
                _current.Insert(classSymbol);
                ScopedSymbolTable classScoped = new ScopedSymbolTable(_current.ScopeLevel + 1,
                    classSymbol.FullName,
                    _current);
                _current = classScoped;
                base.VisitType_declaration(context);
                _current = _current.EnclosingScope;
            }
            else if (context.enum_definition() != null)
            {
                var enumSymbol = EnumSymbol.GetEnumSymbol(context.enum_definition(), modifiers, _current);
                _current.Insert(enumSymbol);
            }
            return null;
        }

        public override object VisitClass_member_declaration([NotNull] Class_member_declarationContext context)
        {
            ScopedSymbolTable funcScoped = null;
            var modifiers = this.GetAllMemberModifiers(context.all_member_modifiers());
            var commonMemberDeclaration = context.common_member_declaration();
            var typedMemberDeclaration = commonMemberDeclaration.typed_member_declaration();
            //have 3 way to declaration function in class
            if ((typedMemberDeclaration != null && typedMemberDeclaration.method_declaration() != null)
                || (commonMemberDeclaration.method_declaration() != null)
                || commonMemberDeclaration.constructor_declaration() != null)
            {
                FuncSymbol symbol = FuncSymbol.GetFuncSymbol(commonMemberDeclaration, modifiers, _current);
                _current.Insert(symbol);
                funcScoped = new ScopedSymbolTable(_current.ScopeLevel + 1,
                symbol.FullName,
                _current);
                _current = funcScoped;
            }
            else if (commonMemberDeclaration.enum_definition() != null)
            {
                EnumSymbol symbol = EnumSymbol.GetEnumSymbol(commonMemberDeclaration.enum_definition(), modifiers, _current);
                _current.Insert(symbol);
            }
            base.VisitClass_member_declaration(context);

            if (funcScoped != null)
            {
                _current = funcScoped.EnclosingScope;
            }
            return null;
        }

        public override object VisitNamespace([NotNull] NamespaceContext context)
        {
            var namespaceName = context.qualified_identifier().GetText();
            //create scopeSymbol if it's not exist

            return base.VisitNamespace(context);
        }


        /// <summary>
        /// Get modifier as public, static,...
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected HashSet<string> GetAllMemberModifiers(All_member_modifiersContext context)
        {
            if (context == null)
                return null;
            //All_member_modifiers node -> All_member_modifier[] node -> name
            HashSet<string> modifiers = new HashSet<string>();
            var modifierContexts = context.all_member_modifier();
            if (modifierContexts != null)
                foreach (var modifierCon in modifierContexts)
                {
                    modifiers.Add(modifierCon.GetText());
                }

            if (modifiers.Count <= 0)
                return null;
            return modifiers;
        }
    }
}
