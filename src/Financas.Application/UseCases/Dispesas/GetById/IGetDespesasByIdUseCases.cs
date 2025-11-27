using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Dispesas.GetById;
public interface IGetDespesasByIdUseCases
{
    Task<ResponseDespesaIdJson> Execute(long id);
}
