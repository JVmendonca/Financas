using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Financas.Infrasctructure.DataAccess.Repositorios;
internal class DespesasRepositorio : IDespesasReadOnlyRepositorio, IDespesasWriteOnlyRepositorio, IDespesasUpdateOnlyRepositorio
{
    private readonly FinancasDbContexto _dbContext;

    public DespesasRepositorio(FinancasDbContexto dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Despesa despesa)
    { 
        _dbContext.Despesas.Add(despesa);
    }

    public async Task Delete(long id)
    {
        var result = await _dbContext.Despesas.FindAsync(id);
        
        _dbContext.Despesas.Remove(result!);
    }
    public async Task<List<Despesa>> GetAll(Domain.Entidades.User user)
    {
       return await _dbContext.Despesas.AsNoTracking().Where(despesa => despesa.UserId == user.Id).ToListAsync();
    }

    async Task<Despesa?> IDespesasReadOnlyRepositorio.GetById(Domain.Entidades.User user, long id)
    {
        return await GetFullDespesa()
            .AsNoTracking()
            .FirstOrDefaultAsync(despesa => despesa.Id == id && despesa.UserId == user.Id);
    }
    async Task<Despesa?> IDespesasUpdateOnlyRepositorio.GetById(Domain.Entidades.User user,long id)
    {
        return await GetFullDespesa()
            .FirstOrDefaultAsync(despesa => despesa.Id == id && despesa.UserId == user.Id);
    }

    public void Update(Despesa despesa)
    {
        _dbContext.Despesas.Update(despesa);
    }

    public async Task<List<Despesa>> FilterByMonth(Domain.Entidades.User user, DateOnly date)
    {
        var startdate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var EndDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Despesas
            .AsNoTracking()
            .Where(d => d.UserId == user.Id && d.Data >= startdate && d.Data <= EndDate)
            .OrderBy(d => d.Data)
            .ThenBy(d => d.Titulo)
            .ToListAsync();
    }

    private IIncludableQueryable<Despesa, ICollection<Tag>> GetFullDespesa()
    {
        return _dbContext.Despesas
           .Include(despesa => despesa.Tags);
    }
}
