using AutoMapper;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;

    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entidades.User>(request);

        return new ResponseRegisteredUserJson
        {
            Nome = user.Nome,
        };
  }

    private void Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        if (result.IsValid == false)
        {
            var errrorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errrorMessages);
        }
    }
}