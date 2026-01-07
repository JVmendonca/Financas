using Financas.Communication.Responses;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Security.Cryptography;
using Financas.Domain.Security.Tokens;
using Financas.Communication.Request;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Login;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPassowordEncripter _passowordEncripter;
    private readonly IAccesTokenGeneretor _accesTokenGeneretor;

    public DoLoginUseCase(IUserReadOnlyRepository repository,
        IPassowordEncripter passowordEncripter,
        IAccesTokenGeneretor accesTokenGeneretor)
    {
        _repository = repository;
        _passowordEncripter = passowordEncripter;
        _accesTokenGeneretor = accesTokenGeneretor;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmai(request.Email);
        if(user is null)
        {
            throw new InavalidLoginExeception();
        }

        var passwordMatch =  _passowordEncripter.Verify(request.Senha, user.Senha);

        if(passwordMatch is false)
        {
            throw new InavalidLoginExeception();
        }

        return new ResponseRegisteredUserJson
        {
            Nome = user.Nome,
            Token = _accesTokenGeneretor.Generate(user)
        };
    
    }


}
