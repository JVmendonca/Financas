using Financas.Domain.Repositorios.User;
using Microsoft.EntityFrameworkCore;

namespace Financas.Infrasctructure.DataAccess.Repositorios.User;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly FinancasDbContexto _dbContext;

    public UserRepository(FinancasDbContexto dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Domain.Entidades.User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }
}
