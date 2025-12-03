using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasUpdateOnlyRepositorio
{
    Task<Dispesa?> GetById(long id);

    void Update(Dispesa dispesa);
}
