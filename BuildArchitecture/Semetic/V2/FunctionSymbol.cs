﻿using Antlr4.Runtime;

namespace BuildArchitecture.Semetic.V2
{
    /** This symbol represents a function ala C, not a method ala Java.
  *  You can associate a node in the parse tree that is responsible
  *  for defining this symbol.
  */
    public class FunctionSymbol : SymbolWithScope, TypedSymbol
    {
        protected ParserRuleContextWithScope defNode;
        protected IType retType;

        public FunctionSymbol(string name) : base(name)
        {
        }

        public void SetDefNode(ParserRuleContextWithScope defNode)
        {
            this.defNode = defNode;
        }

        public ParserRuleContextWithScope GetDefNode()
        {
            return defNode;
        }

        public new IType GetType()
        {
            return retType;
        }

        public void SetType(IType type)
        {
            retType = type;
        }

        /** Return the number of VariableSymbols specifically defined in the scope.
         *  This is useful as either the number of parameters or the number of
         *  parameters and locals depending on how you build the scope tree.
         */
        public int GetNumberOfVariables()
        {
            return Utils.Filter(symbols.Values, s => s is VariableSymbol).Count;
        }

        public int GetNumberOfParameters()
        {
            return Utils.Filter(symbols.Values, s => s is ParameterSymbol).Count;
        }

        public override string ToString() { return name + ":" + base.ToString(); }
    }
}
