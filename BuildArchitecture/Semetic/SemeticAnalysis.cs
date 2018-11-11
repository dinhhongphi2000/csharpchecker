using System;
using Antlr4.Runtime.Misc;

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
            var classSymbol = ClassSymbol.GetClassSymbol(context, _current) as ClassSymbol;
            ScopedSymbolTable classScoped = new ScopedSymbolTable(_current.ScopeLevel + 1, 
                classSymbol.FullName, 
                classSymbol.FullName, 
                _current);
            _current = classScoped;
           base.VisitType_declaration(context);
            _current = _current.EnclosingScope;
            return null;
        }
    }
}
