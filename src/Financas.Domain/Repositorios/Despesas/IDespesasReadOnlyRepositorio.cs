using Financas.Domain.Entidades;
using System.Net.Sockets;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasReadOnlyRepositorio
{
    Task<List<Dispesa>> GetAll();
    Task<Dispesa?> GetById(long id);

    Task<List<Dispesa>> FilterByMonth(DateOnly date);
}
