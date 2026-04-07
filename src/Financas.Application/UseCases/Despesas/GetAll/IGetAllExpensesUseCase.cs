using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Despesas.GetAll;
public interface IGetAllExpensesUseCase
{
    Task<ResponseDespesasjson> Execute();
}
