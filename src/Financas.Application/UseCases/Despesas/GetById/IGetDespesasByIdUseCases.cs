using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Despesas.GetById;
public interface IGetDespesasByIdUseCases
{
    Task<ResponseDespesaIdJson> Execute(long id);
}
