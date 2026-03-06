using AutoMapper;
using Financas.Communication.Responses;
using Financas.Domain.Services.LoggedUser;

namespace Financas.Application.UseCases.User.Get;
public class GetUserUseCase : IGetUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUserUseCase(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();

        return _mapper.Map<ResponseUserProfileJson>(user);
    }
}
