using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasUpdateOnlyRepositorio
{
    Task<Despesa?> GetById(Entidades.User user, long id);
    void Update(Despesa despesa);
}
