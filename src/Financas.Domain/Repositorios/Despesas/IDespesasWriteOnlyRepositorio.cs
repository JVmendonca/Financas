using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasWriteOnlyRepositorio
{
    Task Add(Despesa despesa);
    
    Task Delete(long id);
}
