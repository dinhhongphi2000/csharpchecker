using System;
using System.Collections.Generic;

namespace BuildArchitecture.Semetic
{
    public class DefinitionSymbol : Symbol
    {
        private List<VarSymbol> _properties = null;
        private List<FuncSymbol> _functions = null;

        public string FullName { get; set; }
        public string Namespace { get; set; }
        public List<VarSymbol> Properties
        {
            get
            {
                return _properties;
            }
            set
            {
                if(_properties == null)
                {
                    _properties = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't change value.");
                }
            }
        }
        public List<FuncSymbol> Functions
        {
            get
            {
                return _functions;
            }
            set
            {
                if (_functions == null)
                {
                    _functions = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't change value.");
                }
            }
        }

        public DefinitionSymbol(string name, string fullName, string[] modifier = null, string alias = null)
            : base(name, modifier, alias)
        {
            this.FullName = fullName;
        }

        public DefinitionSymbol(string name, string fullName, List<string> modifier = null, string alias = null)
            : base(name, modifier, alias)
        {
            this.FullName = fullName;
        }
    }
}
