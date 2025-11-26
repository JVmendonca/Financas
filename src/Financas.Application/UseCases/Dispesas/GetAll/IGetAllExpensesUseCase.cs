using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Dispesas.GetAll;
public interface IGetAllExpensesUseCase
{
    Task<ResponseDespesasjson> Execute();
}
