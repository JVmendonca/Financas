using Financas.Communication.Request;
using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Security.Cryptography;
using Financas.Domain.Services.LoggedUser;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentValidation.Results;

namespace Financas.Application.UseCases.User.Password;
public class UpdatePasswordUseCase : IUpdatePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPassowordEncripter _encripter;

    public UpdatePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IPassowordEncripter encripter)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _encripter = encripter;
    }

    public async Task Execute(RequestUpdatePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);
        user.Senha = _encripter.Encrypt(request.NovaSenha);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate (RequestUpdatePasswordJson request, Domain.Entidades.User loggedUser)
    {
        var validator = new UpdatePasswordValidator();

        var result = validator.Validate(request);

        var senhaMatch = _encripter.Verify(request.Senha, loggedUser.Senha);

        if (senhaMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMassages.SENHA_DIFERENTE_DA_ATUAL));
        }

        if (result.IsValid == false)
        {
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(erros);
        }
    }

}
