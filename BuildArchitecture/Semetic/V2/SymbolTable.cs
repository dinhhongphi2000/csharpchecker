namespace BuildArchitecture.Semetic.V2
{
    /** A marginally useful object to track predefined and global scopes. */
    public class SymbolTable
    {
        public static readonly IType INVALID_TYPE = new InvalidType();

        public BaseScope PREDEFINED = new PredefinedScope();
        public GlobalScope GLOBALS;

        public SymbolTable()
        {
            GLOBALS = new GlobalScope(PREDEFINED);
        }

        public void InitTypeSystem()
        {
        }

        public void DefinePredefinedSymbol(ISymbol s)
        {
            PREDEFINED.Define(s);
        }

        public void DefineGlobalSymbol(ISymbol s)
        {
            GLOBALS.Define(s);
        }
    }
}
