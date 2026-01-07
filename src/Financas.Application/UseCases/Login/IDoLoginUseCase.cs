using Financas.Communication.Request;
using Financas.Communication.Responses;

namespace Financas.Application.UseCases.Login;
public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
