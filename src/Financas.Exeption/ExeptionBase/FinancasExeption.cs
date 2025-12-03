namespace Financas.Exeption.ExeptionBase;
public abstract class FinancasExeption : System.Exception
{
    protected FinancasExeption(string message) : base(message)
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErros();
}