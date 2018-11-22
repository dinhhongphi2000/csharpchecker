using Antlr4.Runtime;
using NHibernate.Util;
using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic.V2
{
    /** A symbol representing a collection of data like a struct or class.
  *  Each member has a slot number indexed from 0 and we track data fields
  *  and methods with different slot sequences. A DataAggregateSymbol
  *  can also be a member of an aggregate itself (nested structs, ...).
  */
    public abstract class DataAggregateSymbol : SymbolWithScope, IMemberSymbol, IType
    {
        protected int nextFreeFieldSlot = 0;  // next slot to allocate
        protected int typeIndex;

        public ParserRuleContextWithScope DefNode { get; set; }

        public DataAggregateSymbol(string name) : base(name)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sym"></param>
        /// <exception cref="ArgumentException"></exception>
        public override void Define(ISymbol sym)
        {
            if (!(sym is IMemberSymbol))
            {
                throw new ArgumentException(
                    "sym is " + sym.GetType().Name + " not MemberSymbol"
                );
            }
            base.Define(sym);
            SetSlotNumber(sym);
        }

        public override List<ISymbol> GetSymbols()
        {
            return base.GetSymbols();
        }

        public override LinkedHashMap<string, ISymbol> GetMembers()
        {
            return base.GetMembers();
        }

        /** Look up name within this scope only. Return any kind of MemberSymbol found
         *  or null if nothing with this name found as MemberSymbol.
         */
        public virtual ISymbol ResolveMember(string name)
        {
            ISymbol s = symbols[name];
            if (s is IMemberSymbol)
            {
                return s;
            }
            return null;
        }

        /** Look for a field with this name in this scope only.
         *  Return null if no field found.
         */
        public virtual ISymbol ResolveField(string name)
        {
            ISymbol s = ResolveMember(name);
            if (s is FieldSymbol)
            {
                return s;
            }
            return null;
        }

        /** get the number of fields defined specifically in this class */
        public int GetNumberOfDefinedFields()
        {
            int n = 0;
            foreach (IMemberSymbol s in GetSymbols())
            {
                if (s is FieldSymbol)
                {
                    n++;
                }
            }
            return n;
        }

        /** Get the total number of fields visible to this class */
        public virtual int GetNumberOfFields() { return GetNumberOfDefinedFields(); }

        /** Return the list of fields in this specific aggregate */
        public List<FieldSymbol> GetDefinedFields()
        {
            List<FieldSymbol> fields = new List<FieldSymbol>();
            foreach (IMemberSymbol s in GetSymbols())
            {
                if (s is FieldSymbol)
                {
                    fields.Add((FieldSymbol)s);
                }
            }
            return fields;
        }

        public virtual List<FieldSymbol> GetFields() { return GetDefinedFields(); }

        public virtual void SetSlotNumber(ISymbol sym)
        {
            if (sym is FieldSymbol fsym)
            {
                fsym.slot = nextFreeFieldSlot++;
            }
        }

        public int GetSlotNumber()
        {
            return -1; // class definitions do not yield either field or method slots; they are just nested
        }

        public int GetTypeIndex() { return typeIndex; }

        public void SetTypeIndex(int typeIndex)
        {
            this.typeIndex = typeIndex;
        }
    }
}
