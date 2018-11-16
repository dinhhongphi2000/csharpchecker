using System;

namespace BuildArchitecture.Semetic.V2
{
    /** A field symbol is just a variable that lives inside an aggregate like a
 *  class or struct.
 */
    public class FieldSymbol : VariableSymbol, IMemberSymbol
    {
        public int slot;

        public FieldSymbol(string name) : base(name)
        {
        }

        public int GetSlotNumber() { return slot; }
    }
}
