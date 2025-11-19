using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Dispesas;

namespace Financas.Infrasctructure.DataAccess.Repositorios;
internal class DispesasRepositorio : IDispesasRepositorio
{
    public void add(Dispesa dispesa)
    {
        var dbcontext = new FinancasDbContexto();

        dbcontext.Dispesas.Add(dispesa);

        dbcontext.SaveChanges();
    }
}
