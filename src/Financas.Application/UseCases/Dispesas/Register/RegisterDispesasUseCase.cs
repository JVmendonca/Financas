using AutoMapper;
using Financas.Communication.Enuns;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Domain.Entidades;
using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Dispesas;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterDispesasUseCase : IRegisterDispensaUseCase
{
    private readonly IDispesasRepositorio _repositorio;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterDispesasUseCase(IDispesasRepositorio repositorio, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repositorio = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task <ResponseDispesaJson> Execute(RequestDispesaJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Dispesa>(request);

        await _repositorio.add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseDispesaJson>(entity);
    }
        
    private void Validate(RequestDispesaJson request)
    {
        var validator = new RegisterDispensasValidator();    

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
