using Financas.Communication.Request;

namespace Financas.Application.UseCases.User.Password;
public interface IUpdatePasswordUseCase
{
    Task Execute(RequestUpdatePasswordJson request);
}
