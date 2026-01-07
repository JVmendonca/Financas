namespace Financas.Domain.Repositorios.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistsByEmail(string email);
    Task<Entidades.User?> GetUserByEmai(string email);
}
