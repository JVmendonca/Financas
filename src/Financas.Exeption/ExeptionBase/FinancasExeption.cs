namespace Financas.Exeption.ExeptionBase;
public abstract class FinancasExeption : System.Exception
{
    protected FinancasExeption(string message) : base(message)
    {
        
    }
}
