using Financas.Communication.Request;
using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Dispesas.Register;
public interface IRegisterDispensaUseCase
{
    Task <ResponseDispesaJson> Execute(RequestDispesaJson request);
}
