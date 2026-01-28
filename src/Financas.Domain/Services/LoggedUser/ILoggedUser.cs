using Financas.Domain.Entidades;

namespace Financas.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
