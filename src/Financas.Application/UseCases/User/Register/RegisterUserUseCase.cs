using AutoMapper;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Security.Cryptography;
using Financas.Domain.Security.Tokens;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentValidation.Results;


namespace Financas.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPassowordEncripter _passowordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IAccesTokenGeneretor _tokenGeneretor;

    public RegisterUserUseCase(IMapper mapper, 
        IPassowordEncripter passowordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccesTokenGeneretor tokenGeneretor)
    {
        _mapper = mapper;
        _passowordEncripter = passowordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _UnitOfWork = unitOfWork;
        _tokenGeneretor = tokenGeneretor;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entidades.User>(request);
        user.Senha = _passowordEncripter.Encript(request.Senha);
        user.UserIndetificador = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        await _UnitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Nome = user.Nome,
            Token = _tokenGeneretor.Generate(user)
        };
  }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistsByEmail(request.Email);
        if(emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMassages.EMAIL_JA_EXISTE));
        }

        if (result.IsValid == false)
        {
            var errrorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errrorMessages);
        }
    }
}