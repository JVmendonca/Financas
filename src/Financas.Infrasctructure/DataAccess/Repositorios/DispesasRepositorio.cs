using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Dispesas;

namespace Financas.Infrasctructure.DataAccess.Repositorios;
internal class DispesasRepositorio : IDispesasRepositorio
{
    private readonly FinancasDbContexto _dbContext;
    public DispesasRepositorio(FinancasDbContexto dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task add(Dispesa dispesa)
    {
        _dbContext.Dispesas.Add(dispesa);
    }
}
