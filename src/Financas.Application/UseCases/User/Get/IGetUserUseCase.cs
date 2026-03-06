using Financas.Communication.Responses;

namespace Financas.Application.UseCases.User.Get;
public interface IGetUserUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
