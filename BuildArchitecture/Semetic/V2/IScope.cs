using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /// <summary>
    /// A scope is a dictionary of symbols that are grouped together by some
    /// lexical construct in the input language.Examples include structs,
    /// functions, {...}
    /// code blocks, argument lists, etc...
    ///
    ///
    /// Scopes all have an enclosing scope that encloses them lexically.
    ///
    /// In other words, am I wrapped in a class? a function? a {...}
    /// code block?
    ///
    /// This is distinguished from the parent scope.The parent scope is usually
    /// the enclosing scope, but in the case of inheritance, it is the superclass
    /// rather than the enclosing scope.Otherwise, the global scope would be
    /// considered the parent scope of a class. When resolving symbols, we look
    /// up the parent scope chain not the enclosing scope chain.
    ///
    ///
    /// For convenience of code using this library, I have added a bunch of
    ///  methods one can use to get lots of useful information from a scope, but
    /// they don't necessarily define what a scope is.
    ////
    /// </summary>
    public interface IScope
    {
        /// <summary>
        /// Often scopes have names like function or class names. For
        /// unnamed scopes like code blocks, you can just return "local" or something.
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Scope in which this scope defined. null if no enclosing scope
        /// </summary>
        /// <returns></returns>
        IScope GetEnclosingScope();

        /// <summary>
        /// What scope encloses this scope. E.g., if this scope is a function,
        /// the enclosing scope could be a class. The BaseScope class automatically
        /// adds this to nested scope list of s.
        /// </summary>
        /// <param name="s"></param>
        void SetEnclosingScope(IScope s);

        /// <summary>
        /// Define a symbol in this scope, throw IllegalArgumentException
        /// if sym already defined in this scope.This alters sym:
        ///
        /// 1. Set insertion order number of sym
        /// 2. Set sym's scope to be the scope.
        ///
        /// The order in which symbols are defined must be preserved so that
        /// {@link #getSymbols()} returns the list in definition order.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="sym"></param>
        void Define(ISymbol sym);

        /// <summary>
        /// Look up name in this scope or recursively in parent scope if not here
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISymbol Resolve(string name);

        /// <summary>
        /// Get symbol if name defined within this specific scope
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISymbol GetSymbol(String name);

        /// <summary>
        /// Add a nested local scope to this scope; it's like define() but
        /// for non SymbolWithScope objects.E.g., a FunctionSymbol will
        /// add a LocalScope for its block via this method.
        /// </summary>
        /// <exception cref="ArgumentException">if you pass in a SymbolWithScope.</exception>
        /// <param name="scope"></param>
        void Nest(IScope scope);

        /// <summary>
        /// Return a list of scopes nested within this scope. It has both
        /// ScopedSymbols and scopes without symbols, such as LocalScopes.
        /// This returns a superset or same set as {@link #getNestedScopedSymbols}.
        /// ScopedSymbols come first then all non-ScopedSymbols Scope objects.
        ///
        /// Insertion order is used within each sublist.
        /// </summary>
        /// <returns></returns>
        List<IScope> GetNestedScopes();

        // ------------ Convenience methods --------------------------------

        /// <summary>
        /// Return (inclusive) list of all scopes on path to root scope.
        /// The first element is the current scope and the last is the root scope.
        /// </summary>
        /// <returns></returns>
        List<IScope> GetEnclosingPathToRoot();


        /// <summary>
        /// Return all immediately enclosed scoped symbols in insertion order.
        /// E.g., a class would return all nested classes and any
        /// methods.There does not have to be an explicit pointer to
        /// the nested scopes. This method generally searches the list
        /// of symbols looking for symbols that implement Scope. Gets
        /// only those scopes that are in the symbols list of "this"
        /// scope.E.g., does not get local scopes within a function.
        /// This returns a subset or same set as {@link #getNestedScopes}.
        /// </summary>
        /// <returns></returns>
        List<IScope> GetNestedScopedSymbols();

        /// <summary>
        /// Return the symbols defined within this scope. The order of insertion
        /// into the scope is the order returned in this list.
        /// </summary>
        /// <returns></returns>
        List<ISymbol> GetSymbols();

        /// <summary>
        /// Return all symbols found in all nested scopes. The order
        /// of insertion into the scope is the order returned in this
        /// list for each scope.The scopes are traversed in the
        /// order in which they are encountered in the input.
        /// </summary>
        /// <returns></returns>
        List<ISymbol> GetAllSymbols();

        /// <summary>
        /// Return the set of names associated with all symbols in the scope.
        /// </summary>
        /// <returns></returns>
        List<string> GetSymbolNames();

        /// <summary>
        /// Number of symbols in this specific scope
        /// </summary>
        /// <returns></returns>
        int GetNumberOfSymbols();

        /// <summary>
        /// Return scopes from to current with separator in between
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        string ToQualifierString(String separator);
    }
}
