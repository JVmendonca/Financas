using AutoMapper;
using Financas.Communication.Request;
using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Despesas;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Update;
public class UpdateDespesaUseCase : IUpdateDespesaUseCase
{
   private readonly IMapper _mapper;
   private readonly IUnitOfWork _unitOfWork;
   private readonly IDespesasUpdateOnlyRepositorio _repositorio;

    public UpdateDespesaUseCase(IMapper mapper, IUnitOfWork unitOfWork, IDespesasUpdateOnlyRepositorio repositorio)
    {
          _mapper = mapper;
          _unitOfWork = unitOfWork; 
          _repositorio = repositorio;
    }

    public async Task Execute(long id, RequestDispesaJson request)
    {
        Validate(request);

        var despesa = await _repositorio.GetById(id);

        if(despesa is null)
        {
            throw new NotFoundExeption($"Despesa com id {id} não encontrada.");
        }
        
        _mapper.Map(request, despesa);

        _repositorio.Update(despesa);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestDispesaJson request)
    {
        var validator = new DespensasValidator();

        var result = validator.Validate(request);

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
