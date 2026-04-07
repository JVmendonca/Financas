namespace Financas.Application.UseCases.Despesas.Delete;
public interface IDeleteDespesaUseCase
{
    Task Execute(long id);
}
