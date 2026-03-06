namespace Financas.Domain.Repositorios.User;
public interface IUserUpdateOnlyRepository
{
    Task<Entidades.User> GetById(long id);
    void Update(Entidades.User user);
}
