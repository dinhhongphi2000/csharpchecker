using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildArchitecture.Semetic.V2
{
    /// <summary>
    /// LinerScope find a symbol in scope that declare on other tree(file)
    /// </summary>
    public class LinkerScopeCollection
    {
        private readonly Dictionary<string, List<IScope>> linker;

        public LinkerScopeCollection()
        {
            linker = new Dictionary<string, List<IScope>>();
        }

        public List<string> GetKeys()
        {
            if (linker != null)
                return linker.Keys.ToList();
            return null;
        }

        public List<IScope> this[string treeName]
        {
            get
            {
                return linker[treeName];
            }
            set
            {
                linker[treeName] = value;
            }
        }

        public ISymbol Resolve(string scopeName, string symbolName)
        {
            foreach(var item in linker)
            {
                foreach (var scope in item.Value)
                {
                    if (scope.ToQualifierString(".") == scopeName)
                    {
                        return ((DataAggregateSymbol)scope).ResolveMember(symbolName);
                    }
                }
            }
            return null;
        }
    }
}
