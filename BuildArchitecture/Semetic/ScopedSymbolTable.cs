﻿using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class ScopedSymbolTable
    {
        //symbol(variable, class, interface,...) which this scope own.
        private Dictionary<string, Symbol> _symbols;

        public int ScopeLevel { get; private set; }
        public string ScopeName { get; private set; }
        //Show how we go to this scope
        public string Path { get; private set; }
        //Parent scope
        public ScopedSymbolTable EnclosingScope { get; private set; }

        public ScopedSymbolTable(int scopeLevel, string scopeName, string path, ScopedSymbolTable enclosingCope = null)
        {
            this.ScopeLevel = scopeLevel;
            this.ScopeName = scopeName;
            this.Path = path;
            this.EnclosingScope = enclosingCope;
            this._symbols = new Dictionary<string, Symbol>();
        }

        /// <summary>
        /// Find symbol with name
        /// </summary>
        /// <param name="name">name of symbol</param>
        /// <returns>Symbol of interface, class, struct,...</returns>
        public Symbol Lookup(string name)
        {
            var symbol = this._symbols[name];
            if (symbol != null)
                return symbol;

            if (this.EnclosingScope != null)
                return this.EnclosingScope.Lookup(name);
            return null;
        }

        /// <summary>
        /// Insert symbol to current scope
        /// </summary>
        /// <param name="symbol"></param>
        public void Insert(Symbol symbol)
        {
            this._symbols[symbol.Name] = symbol;
        }
    }
}
