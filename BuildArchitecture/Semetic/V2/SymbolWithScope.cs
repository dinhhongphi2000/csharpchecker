using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /** An abstract base class that houses common functionality for
 *  symbols like classes and functions that are both symbols and scopes.
 *  There is some common cut and paste functionality with {@link BaseSymbol}
 *  because of a lack of multiple inheritance in Java but it is minimal.
 */
    public abstract class SymbolWithScope : BaseScope, ISymbol, IScope
    {
        protected readonly string name; // All symbols at least have a name
        protected int index;    // insertion order from 0; compilers often need this

        public SymbolWithScope(string name)
        {
            this.name = name;
        }

        public override string GetName() { return name; }
        public IScope GetScope() { return enclosingScope; }
        public void SetScope(IScope scope) { SetEnclosingScope(scope); }

        public override IScope GetEnclosingScope() { return enclosingScope; }

        /** Return the name prefixed with the name of its enclosing scope
         *  using '.' (dot) as the scope separator.
         */
        public string GetQualifiedName()
        {
            return enclosingScope.GetName() + "." + name;
        }

        /** Return the name prefixed with the name of its enclosing scope. */
        public string getQualifiedName(string scopePathSeparator)
        {
            return enclosingScope.GetName() + scopePathSeparator + name;
        }

        /** Return the fully qualified name includes all scopes from the root down
         *  to this particular symbol.
         */
        public string GetFullyQualifiedName(string scopePathSeparator)
        {
            List<IScope> path = GetEnclosingPathToRoot();
            path.Reverse();
            return Utils.JoinScopeNames(path, scopePathSeparator);
        }

        public int HetInsertionOrderNumber()
        {
            return index;
        }

        public void SetInsertionOrderNumber(int i)
        {
            this.index = i;
        }

        public override int GetNumberOfSymbols()
        {
            return symbols.Count;
        }

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

        public int HashCode()
        {
            return name.GetHashCode();
        }

        public abstract int GetInsertionOrderNumber();
    }
}
