using System.Net;

namespace Financas.Exeption.ExeptionBase;
public class ErrorOnValidationException : FinancasExeption
{

    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErros()
    {
        return _errors;
    }
}
