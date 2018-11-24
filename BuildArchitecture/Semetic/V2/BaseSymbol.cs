using Antlr4.Runtime;
using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    public abstract class BaseSymbol : ISymbol
    {
        protected readonly string name;          // All symbols at least have a name
        protected IType type;                 // If language statically typed, record type
        protected IScope scope;               // All symbols know what scope contains them.
        protected int lexicalOrder;          // order seen or insertion order from 0; compilers often need this

        public ParserRuleContext DefNode { get; set; } // points at definition node in tree

        public BaseSymbol(string name) { this.name = name; }

        public string GetName() { return name; }
        public IScope GetScope() { return scope; }
        public void SetScope(IScope scope) { this.scope = scope; }

        public virtual IType GetSymbolType() { return type; }
        public virtual void SetType(IType type) { this.type = type; }

        public override bool Equals(object obj)
        {
            if (!(obj is ISymbol))
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }
            return name.Equals(((ISymbol)obj).GetName());
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public int GetInsertionOrderNumber()
        {
            return lexicalOrder;
        }

        public void SetInsertionOrderNumber(int i)
        {
            this.lexicalOrder = i;
        }

        public virtual string GetFullyQualifiedName(string scopePathSeparator)
        {
            List<IScope> path = scope.GetEnclosingPathToRoot();
            path.Reverse();
            string qualifier = Utils.JoinScopeNames(path, scopePathSeparator);
            return qualifier + scopePathSeparator + name;
        }

        public override string ToString()
        {
            string s = "";
            if (scope != null) s = scope.GetName() + ".";
            if (type != null)
            {
                string ts = type.ToString();
                if (type is SymbolWithScope ) {
                    ts = ((SymbolWithScope)type).GetFullyQualifiedName(".");
                }
                return '<' + s + GetName() + ":" + ts + '>';
            }
            return s + GetName();
        }
    }
}
