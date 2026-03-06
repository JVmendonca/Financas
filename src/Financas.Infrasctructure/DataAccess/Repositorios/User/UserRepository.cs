using Financas.Domain.Repositorios.User;
using Microsoft.EntityFrameworkCore;

namespace Financas.Infrasctructure.DataAccess.Repositorios.User;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
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

    public async Task<Financas.Domain.Entidades.User> GetById(long Id)
    {
        return await _dbContext.Users.FirstAsync(user => user.Id == Id);
    }

    public async Task<Domain.Entidades.User?> GetUserByEmai(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public void Update(Domain.Entidades.User user)
    {
        _dbContext.Users.Update(user);
    }
}
