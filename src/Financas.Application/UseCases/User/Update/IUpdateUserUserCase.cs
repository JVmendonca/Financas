using Financas.Communication.Request;

namespace Financas.Application.UseCases.User.Update;
public interface IUpdateUserUserCase
{
    Task Execute(RequestUpdateUserJson request);
}
