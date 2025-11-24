using Financas.Domain.Repositorios;

namespace Financas.Infrasctructure.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly FinancasDbContexto _dbContext;
    public UnitOfWork(FinancasDbContexto dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
