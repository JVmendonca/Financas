using Financas.Communication.Request;
using Financas.Communication.Responses;

namespace Financas.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
