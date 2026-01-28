using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Services.LoggedUser;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Delete;
public class DeleteDespesaUseCase : IDeleteDespesaUseCase
{
    private readonly IDespesasReadOnlyRepositorio _readOnlyRepositorio;
    private readonly IDespesasWriteOnlyRepositorio _repositorio;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteDespesaUseCase(
        IDespesasWriteOnlyRepositorio repositorio,
        IUnitOfWork unitOfWork, 
        ILoggedUser loggedUser,
        IDespesasReadOnlyRepositorio readOnlyRepositorio)
    {
        _repositorio = repositorio;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _readOnlyRepositorio = readOnlyRepositorio;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var despesas = await _readOnlyRepositorio.GetById(loggedUser, id);
        if (despesas is null)
        {
            throw new NotFoundExeption(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA);
        }
        await _repositorio.Delete(id);

        await _unitOfWork.Commit();
    }
    
}
