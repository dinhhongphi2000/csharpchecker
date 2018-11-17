using NHibernate.Util;
using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /** An abstract base class that houses common functionality for scopes. */
    public abstract class BaseScope : IScope
    {
        protected IScope enclosingScope; // null if this scope is the root of the scope tree

        /** All symbols defined in this scope; can include classes, functions,
         *  variables, or anything else that is a ISymbol impl. It does NOT
         *  include non-ISymbol-based things like LocalScope. See nestedScopes.
         */
        protected LinkedHashMap<String, ISymbol> symbols = new LinkedHashMap<string, ISymbol>();

        /** All directly contained scopes, typically LocalScopes within a
         *  LocalScope or a LocalScope within a FunctionSymbol. This does not
         *  include SymbolWithScope objects.
         */
        protected List<IScope> nestedScopesNotSymbols = new List<IScope>();

        public BaseScope() { }

        public BaseScope(IScope enclosingScope) { SetEnclosingScope(enclosingScope); }

        public virtual LinkedHashMap<String, ISymbol> GetMembers()
        {
            return symbols;
        }

        public ISymbol GetSymbol(String name)
        {
            return symbols[name];
        }

        public void SetEnclosingScope(IScope enclosingScope)
        {
            this.enclosingScope = enclosingScope;
        }

        public List<IScope> getAllNestedScopedSymbols()
        {
            List<IScope> scopes = new List<IScope>();
            Utils.GetAllNestedScopedSymbols(this, scopes);
            return scopes;
        }

        public List<IScope> GetNestedScopedSymbols()
        {
            List<ISymbol> symbols = Utils.Filter(GetSymbols(), s => s is IScope);
            List<IScope> scopes = new List<IScope>();
            foreach(ISymbol item in scopes)
            {
                scopes.Add(item.GetScope());
            }
            return (List<IScope>)scopes; // force it to cast
        }

        public List<IScope> GetNestedScopes()
        {
            List<IScope> all = new List<IScope>();
            all.AddRange(GetNestedScopedSymbols());
            all.AddRange(nestedScopesNotSymbols);
            return all;
        }


        /// <summary>
        /// Add a nested scope to this scope; could also be a FunctionSymbol
        /// if your language allows nested functions.
        /// </summary>
        /// <param name="scope"></param>        
        /// <exception cref="ArgumentException"></exception>
        public void Nest(IScope scope)
        {
            if (scope is SymbolWithScope)
            {
                throw new ArgumentException("Add SymbolWithScope instance " +
                                                       scope.GetName() + " via define()");
            }
            nestedScopesNotSymbols.Add(scope);
        }

        public virtual ISymbol Resolve(String name)
        {
            ISymbol s = symbols[name];
            if (s != null)
            {
                return s;
            }
            // if not here, check any enclosing scope
            IScope parent = GetEnclosingScope();
            if (parent != null) return parent.Resolve(name);
            return null; // not found
        }

        /// <summary>
        /// </summary>
        /// <param name="sym"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Define(ISymbol sym)
        {
            if (symbols.ContainsKey(sym.GetName()))
            {
                throw new ArgumentException("duplicate symbol " + sym.GetName());
            }
            sym.SetScope(this);
            sym.SetInsertionOrderNumber(symbols.Count); // set to insertion position from 0
            symbols.Add(sym.GetName(), sym);
        }

        public virtual IScope GetEnclosingScope() { return enclosingScope; }

        /** Walk up enclosingScope until we find topmost. Note this is
         *  enclosing scope not necessarily parent. This will usually be
         *  a global scope or something, depending on your scope tree.
         */
        public virtual IScope GetOuterMostEnclosingScope()
        {
            IScope s = this;
            while (s.GetEnclosingScope() != null)
            {
                s = s.GetEnclosingScope();
            }
            return s;
        }

        /** Walk up enclosingScope until we find an object of a specific type.
         *  E.g., if you want to get enclosing method, you would pass in
         *  MethodSymbol.class, unless of course you have created a subclass for
         *  your language implementation.
         */
        public virtual MethodSymbol GetEnclosingScopeOfType(Type type)
        {
            IScope s = this;
            while (s != null)
            {
                if (s.GetType() == type)
                {
                    return (MethodSymbol)s;
                }
                s = s.GetEnclosingScope();
            }
            return null;
        }

        public virtual List<IScope> GetEnclosingPathToRoot()
        {
            List<IScope> scopes = new List<IScope>();
            IScope s = this;
            while (s != null)
            {
                scopes.Add(s);
                s = s.GetEnclosingScope();
            }
            return scopes;
        }

        public virtual List<ISymbol> GetSymbols()
        {
            IEnumerable<ISymbol> values = symbols.Values;
            if (values is List<ISymbol>)
            {
                return (List<ISymbol>)values;
            }
            return new List<ISymbol>(values);
        }

        public virtual List<ISymbol> GetAllSymbols()
        {
            List<ISymbol> syms = new List<ISymbol>();
            syms.AddRange(GetSymbols());
            foreach (ISymbol s in symbols.Values)
            {
                if (s is IScope)
                {
                    IScope scope = (IScope)s;
                    syms.AddRange(scope.GetAllSymbols());
                }
            }
            return syms;
        }

        public virtual int GetNumberOfSymbols()
        {
            return symbols.Count;
        }

        public virtual List<string> GetSymbolNames()
        {
            return new List<string>(symbols.Keys);
        }

        public override string ToString() { return symbols.Keys.ToString(); }

        public string ToScopeStackString(string separator)
        {
            return Utils.toScopeStackString(this, separator);
        }

        public string ToQualifierString(string separator)
        {
            return Utils.ToQualifierString(this, separator);
        }

        public string ToTestString()
        {
            return ToTestString(", ", ".");
        }

        public string ToTestString(String separator, String scopePathSeparator)
        {
            List<ISymbol> allSymbols = this.GetAllSymbols();
            List<String> syms = Utils.Map(allSymbols, s => s.GetScope().GetName() + scopePathSeparator + s.GetName());
            return Utils.Join(syms, separator);
        }

        public abstract string GetName();
    }
}
