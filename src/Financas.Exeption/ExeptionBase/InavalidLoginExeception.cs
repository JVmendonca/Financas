using System.Net;

namespace Financas.Exeption.ExeptionBase;
public class InavalidLoginExeception : FinancasExeption
{
    public InavalidLoginExeception() : base(ResourceErrorMassages.EMAIL_OU_SENHA_ERRADO)
    {
    }
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErros()
    {
        return [Message];
    }
}

