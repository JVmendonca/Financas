using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Despesas;
using Financas.Infrasctructure.Services;
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

    public async Task Delete(long id)
    {
        var result = await _dbContext.Dispesas.FindAsync(id);
        
        _dbContext.Dispesas.Remove(result!);
    }
    public async Task<List<Dispesa>> GetAll(Domain.Entidades.User user)
    {
       return await _dbContext.Dispesas.AsNoTracking().Where(despesa => despesa.UserId == user.Id).ToListAsync();
    }

    async Task<Dispesa?> IDespesasReadOnlyRepositorio.GetById(Domain.Entidades.User user, long id)
    {
        return await _dbContext.Dispesas
            .AsNoTracking()
            .FirstOrDefaultAsync(despesa => despesa.Id == id && despesa.UserId == user.Id);
    }
    async Task<Dispesa?> IDespesasUpdateOnlyRepositorio.GetById(Domain.Entidades.User user,long id)
    {
        return await _dbContext.Dispesas.FirstOrDefaultAsync(despesa => despesa.Id == id && despesa.UserId == user.Id);
    }

    public void Update(Dispesa dispesa)
    {
        _dbContext.Dispesas.Update(dispesa);
    }

    public async Task<List<Dispesa>> FilterByMonth(Domain.Entidades.User user, DateOnly date)
    {
        var startdate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var EndDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Dispesas
            .AsNoTracking()
            .Where(d => d.UserId == user.Id && d.Data >= startdate && d.Data <= EndDate)
            .OrderBy(d => d.Data)
            .ThenBy(d => d.Titulo)
            .ToListAsync();
    }
}
