using Financas.Domain.Entidades;
using System.Net.Sockets;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasReadOnlyRepositorio
{
    Task<List<Despesa>> GetAll(Entidades.User user);
    Task<Despesa?> GetById(Entidades.User user,long id);
    Task<List<Despesa>> FilterByMonth(Entidades.User user, DateOnly date);
}
