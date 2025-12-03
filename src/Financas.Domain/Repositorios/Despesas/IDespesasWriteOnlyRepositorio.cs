using Financas.Domain.Entidades;

namespace Financas.Domain.Repositorios.Despesas;
public interface IDespesasWriteOnlyRepositorio
{
    Task add(Dispesa dispesa);
    /// <summary>
    ///  essa função deleta uma despesa pelo id se for um sucesso retorna true se não false
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}
