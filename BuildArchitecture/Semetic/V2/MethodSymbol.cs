using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /** A method symbol is a function that lives within an aggregate/class and has a slot number. */
    public class MethodSymbol : FunctionSymbol, IMemberSymbol
    {
        public int slot = -1;

        public MethodSymbol(string name) : base(name)
        {
        }

        public int GetSlotNumber() { return slot; }

        
    }
}
