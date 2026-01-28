namespace Financas.Domain.Security.Tokens;
public interface ITokenProvider
{
    string TokenOnRequest();
}
