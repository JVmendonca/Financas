using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasReadOnlyRepositorio
{
    Task<List<Dispesa>> GetAll();
    Task<Dispesa?> GetById(long id);
}
