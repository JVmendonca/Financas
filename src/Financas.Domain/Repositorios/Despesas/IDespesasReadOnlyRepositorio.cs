using Financas.Domain.Entidades;
using System.Net.Sockets;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasReadOnlyRepositorio
{
    Task<List<Dispesa>> GetAll(Entidades.User user);
    Task<Dispesa?> GetById(Entidades.User user,long id);
    Task<List<Dispesa>> FilterByMonth(Entidades.User user, DateOnly date);
}
