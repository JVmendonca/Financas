using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Services.LoggedUser;

namespace Financas.Application.UseCases.User.Delete;
public class DeleteProfileUseCase : IDeleteProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProfileUseCase(
        IUnitOfWork unitOfWork,
        IUserWriteOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();
    }
}
