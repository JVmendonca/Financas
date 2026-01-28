using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasWriteOnlyRepositorio
{
    Task add(Dispesa dispesa);
    
    Task Delete(long id);
}
