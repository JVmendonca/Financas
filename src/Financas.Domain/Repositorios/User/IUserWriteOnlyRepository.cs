namespace Financas.Domain.Repositorios.User;
public interface IUserWriteOnlyRepository
{
    Task Add(Domain.Entidades.User user);

    Task Delete(Domain.Entidades.User user);  
}
