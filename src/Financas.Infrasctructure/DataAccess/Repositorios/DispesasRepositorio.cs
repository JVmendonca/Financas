using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Dispesas;
using Microsoft.EntityFrameworkCore;

namespace Financas.Infrasctructure.DataAccess.Repositorios;
internal class DispesasRepositorio : IDespesasRepositorio
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

    public async Task<List<Dispesa>> GetAll()
    {
       return await _dbContext.Dispesas.ToListAsync();
    }
}
