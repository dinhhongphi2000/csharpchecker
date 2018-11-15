using Iesi.Collections.Generic;
using System.Collections.Generic;
using System.Linq;

namespace BuildArchitecture.Semetic.V2
{
    /** A ISymbol representing the class. It is a kind of data aggregate
 *  that has much in common with a struct.
 */
    public class ClassSymbol : DataAggregateSymbol
    {

        protected string superClassName; // null if this is Object
        protected int nextFreeMethodSlot = 0; // next slot to allocate

        public ClassSymbol(string name) : base(name)
        {
        }

        /** Return the ClassSymbol associated with superClassName or null if
         *  superclass is not resolved looking up the enclosing scope chain.
         */
        public ClassSymbol GetSuperClassScope()
        {
            if (superClassName != null)
            {
                if (GetEnclosingScope() != null)
                {
                    ISymbol superClass = GetEnclosingScope().Resolve(superClassName);
                    if (superClass is ClassSymbol)
                    {
                        return (ClassSymbol)superClass;
                    }
                }
            }
            return null;
        }

        /** Multiple superclass or interface implementations and the like... */
        public IEnumerable<ClassSymbol> GetSuperClassScopes()
        {
            ClassSymbol superClassScope = GetSuperClassScope();
            if (superClassScope != null)
            {
                List<ClassSymbol> list = new List<ClassSymbol>();
                list.Add(superClassScope);
                return list;
            }
            return null;
        }

        public override ISymbol Resolve(string name)
        {
            ISymbol s = ResolveMember(name);
            if (s != null)
            {
                return s;
            }
            // if not a member, check any enclosing scope. it might be a global variable for example
            IScope parent = GetEnclosingScope();
            if (parent != null) return parent.Resolve(name);
            return null; // not found
        }

        /** Look for a member with this name in this scope or any super class.
         *  Return null if no member found.
         */
        public override ISymbol ResolveMember(string name)
        {
            ISymbol s = symbols[name];
            if (s is IMemberSymbol)
            {
                return s;
            }
            // walk superclass chain
            List<ClassSymbol> superClassScopes = GetSuperClassScopes().ToList();
            if (superClassScopes != null)
            {
                foreach (ClassSymbol sup in superClassScopes)
                {
                    s = sup.ResolveMember(name);
                    if (s is IMemberSymbol)
                    {
                        return s;
                    }
                }
            }
            return null;
        }

        /** Look for a field with this name in this scope or any super class.
         *  Return null if no field found.
         */
        public override ISymbol ResolveField(string name)
        {
            ISymbol s = ResolveMember(name);
            if (s is FieldSymbol)
            {
                return s;
            }
            return null;
        }

        /** Look for a method with this name in this scope or any super class.
         *  Return null if no method found.
         */
        public MethodSymbol ResolveMethod(string name)
        {
            ISymbol s = ResolveMember(name);
            if (s is MethodSymbol)
            {
                return (MethodSymbol)s;
            }
            return null;
        }

        public void SetSuperClass(string superClassName)
        {
            this.superClassName = superClassName;
            nextFreeMethodSlot = GetNumberOfMethods();
        }

        public string GetSuperClassName()
        {
            return superClassName;
        }

        public override void SetSlotNumber(ISymbol sym)
        {
            if (sym is MethodSymbol msym)
            {
                // handle inheritance. If not found in this scope, check superclass
                // if any.
                ClassSymbol superClass = GetSuperClassScope();
                if (superClass != null)
                {
                    MethodSymbol superMethodSym = superClass.ResolveMethod(sym.GetName());
                    if (superMethodSym != null)
                    {
                        msym.slot = superMethodSym.slot;
                    }
                }
                if (msym.slot == -1)
                {
                    msym.slot = nextFreeMethodSlot++;
                }
            }
            else
            {
                base.SetSlotNumber(sym);
            }
        }

        /** Return the set of all methods defined within this class */
        public IEnumerable<MethodSymbol> GetDefinedMethods()
        {
            HashSet<MethodSymbol> methods = new HashSet<MethodSymbol>();
            foreach (IMemberSymbol s in GetSymbols())
            {
                if (s is MethodSymbol mes)
                {
                    methods.Add(mes);
                }
            }
            return methods;
        }

        /** Return the set of all methods either inherited or not */
        public IEnumerable<MethodSymbol> GetMethods()
        {
            LinkedHashSet<MethodSymbol> methods = new LinkedHashSet<MethodSymbol>();
            ClassSymbol superClassScope = GetSuperClassScope();
            IEnumerable<MethodSymbol> temp;
            if (superClassScope != null)
            {
                temp = superClassScope.GetMethods();
                foreach (var item in temp)
                {
                    methods.Add(item);
                }
            }
            temp = GetDefinedMethods();
            if (temp != null)
            {
                foreach (var item in temp)
                {
                    methods.Remove(item); // override method from superclass
                }
                temp = GetDefinedMethods();
                foreach (var item in temp)
                {
                    methods.Remove(item); // override method from superclass
                }
            }
            return methods;
        }

        public override List<FieldSymbol> GetFields()
        {
            List<FieldSymbol> fields = new List<FieldSymbol>();
            ClassSymbol superClassScope = GetSuperClassScope();
            if (superClassScope != null)
            {
                fields.AddRange(superClassScope.GetFields());
            }
            fields.AddRange(GetDefinedFields());
            return fields;
        }

        /** get the number of methods defined specifically in this class */
        public int GetNumberOfDefinedMethods()
        {
            int n = 0;
            foreach (IMemberSymbol s in GetSymbols())
            {
                if (s is MethodSymbol)
                {
                    n++;
                }
            }
            return n;
        }

        /** get the total number of methods visible to this class */
        public int GetNumberOfMethods()
        {
            int n = 0;
            ClassSymbol superClassScope = GetSuperClassScope();
            if (superClassScope != null)
            {
                n += superClassScope.GetNumberOfMethods();
            }
            n += GetNumberOfDefinedMethods();
            return n;
        }

        public override int GetNumberOfFields()
        {
            int n = 0;
            ClassSymbol superClassScope = GetSuperClassScope();
            if (superClassScope != null)
            {
                n += superClassScope.GetNumberOfFields();
            }
            n += GetNumberOfDefinedFields();
            return n;
        }

        public override string ToString()
        {
            return name + ":" + base.ToString();
        }
    }
}
