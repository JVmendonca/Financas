using System.Net;

namespace Financas.Exeption.ExeptionBase;
public class NotFoundExeption : FinancasExeption
{
    public NotFoundExeption(string massage) : base(massage)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErros()
    {
        return [Message];
    }
}
