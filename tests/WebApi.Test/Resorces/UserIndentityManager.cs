using Financas.Domain.Entidades;

namespace WebApi.Test.Resorces;
public class UserIndentityManager
{
    private readonly Financas.Domain.Entidades.User _user;
    private readonly string _password;
    private readonly string _token;

    public UserIndentityManager(User user, string password, string token)
    {
        _user = user;
        _password = password;
        _token = token;
    }

    public string GetEmail() => _user.Email;
    public string GetName() => _user.Nome;
    public string GetSenha() => _password;
    public string GetToken() => _token;

}
