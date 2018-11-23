using System;

namespace BuildArchitecture.Semetic.V2
{
    /// <summary>
    /// A generic programming language symbol. A symbol has to have a name and
    ///a scope in which it lives.It also helps to know the order in which
    ///symbols are added to a scope because this often translates to
    ///register or parameter numbers.
    /// </summary>
    public interface ISymbol
    {
        string GetName();
        IScope GetScope();
        void SetScope(IScope scope); // set scope (not enclosing) for this symbol; who contains it?
        int GetInsertionOrderNumber(); // index showing insertion order from 0
        void SetInsertionOrderNumber(int i);
        string GetFullyQualifiedName(string scopePathSeparator);

        // to satisfy adding symbols to sets, hashtables
        int GetHashCode();
        bool Equals(Object o);
    }
}
