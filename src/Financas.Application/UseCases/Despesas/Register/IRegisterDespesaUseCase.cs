using Financas.Communication.Request;
using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Despesas.Register;
public interface IRegisterDespesaUseCase
{
    Task <ResponseDespesaJson> Execute(RequestDespesaJson request);
}
