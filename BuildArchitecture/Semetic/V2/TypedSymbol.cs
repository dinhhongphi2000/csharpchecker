namespace BuildArchitecture.Semetic.V2
{
    /// <summary>
    /// This interface tags user-defined symbols that have static type information,
    /// like variables and functions.
    /// </summary>
    public interface TypedSymbol
    {
        IType GetType();
        void SetType(IType type);
    }
}
