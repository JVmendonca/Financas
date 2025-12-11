using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Despesas;
using Microsoft.EntityFrameworkCore;

namespace Financas.Infrasctructure.DataAccess.Repositorios;
internal class DispesasRepositorio : IDespesasReadOnlyRepositorio, IDespesasWriteOnlyRepositorio, IDespesasUpdateOnlyRepositorio
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

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Dispesas.FirstOrDefaultAsync(d => d.Id == id);
        if (result == null)
        {
            return false;
        }
        _dbContext.Dispesas.Remove(result);
        return true;
    }
    public async Task<List<Dispesa>> GetAll()
    {
       return await _dbContext.Dispesas.AsNoTracking().ToListAsync();
    }

    async Task<Dispesa?> IDespesasReadOnlyRepositorio.GetById(long id)
    {
        return await _dbContext.Dispesas.AsNoTracking().FirstOrDefaultAsync(despesa => despesa.Id == id);
    }
    async Task<Dispesa?> IDespesasUpdateOnlyRepositorio.GetById(long id)
    {
        return await _dbContext.Dispesas.FirstOrDefaultAsync(despesa => despesa.Id == id);
    }

    public void Update(Dispesa dispesa)
    {
        _dbContext.Dispesas.Update(dispesa);
    }

    public async Task<List<Dispesa>> FilterByMonth(DateOnly date)
    {
        var startdate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var EndDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Dispesas
            .AsNoTracking()
            .Where(d => d.Data >= startdate && d.Data <= EndDate)
            .OrderBy(d => d.Data)
            .ToListAsync();
    }
}
