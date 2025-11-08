namespace Financas.Exeption.ExeptionBase;
public class ErrorOnValidationException : FinancasExeption
{

    public List<string> Errors { get; set; }
    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
