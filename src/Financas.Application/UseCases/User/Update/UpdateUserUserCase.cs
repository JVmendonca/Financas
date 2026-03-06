using Financas.Communication.Request;
using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Services.LoggedUser;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.User.Update;
public class UpdateUserUserCase : IUpdateUserUserCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUserCase(
        ILoggedUser loggedUser,
        IUserUpdateOnlyRepository userUpdateOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = userUpdateOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();

        await Validator(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Nome = request.Nome;
        user.Email = request.Email!;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validator(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _userReadOnlyRepository.ExistsByEmail(request.Email);
            if (userExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMassages.EMAIL_JA_EM_USO));
        }    

        if (result.IsValid == false)
        {
           var errorsMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

           throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
