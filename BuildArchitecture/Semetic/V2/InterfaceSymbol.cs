using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture.Semetic.V2
{
    public class InterfaceSymbol : DataAggregateSymbol
    {
        private List<string> baseInterfaceName;

        public InterfaceSymbol(string name) : base(name)
        {

        }

        public void AddBaseName(string baseName)
        {
            if (baseInterfaceName == null)
                baseInterfaceName = new List<string>();
            baseInterfaceName.Add(baseName);
        }

        public List<InterfaceSymbol> GetBaseScopeName()
        {
            if (baseInterfaceName != null)
            {
                if (GetEnclosingScope() != null)
                {
                    List<InterfaceSymbol> baseInterfaceList = new List<InterfaceSymbol>();
                    foreach (var item in baseInterfaceName)
                    {
                        ISymbol baseInterface = GetEnclosingScope().Resolve(item);
                        if (baseInterface is InterfaceSymbol)
                        {
                            baseInterfaceList.Add((InterfaceSymbol)baseInterface);
                        }
                    }
                    return baseInterfaceList;
                }
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
    }
}
