using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Despesas;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Delete;
public class DeleteDespesaUseCase : IDeleteDespesaUseCase
{
    private readonly IDespesasWriteOnlyRepositorio _repositorio;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDespesaUseCase(
        IDespesasWriteOnlyRepositorio repositorio,
        IUnitOfWork unitOfWork)
    {
        _repositorio = repositorio;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repositorio.Delete(id);

        if (result == false)
        {
            throw new NotFoundExeption(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA);
        }
        await _unitOfWork.Commit();
    }
    
}
