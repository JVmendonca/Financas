using AutoMapper;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Domain.Entidades;
using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Services.LoggedUser;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterDispesasUseCase : IRegisterDispensaUseCase
{
    private readonly IDespesasWriteOnlyRepositorio _repositorio;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterDispesasUseCase(IDespesasWriteOnlyRepositorio repositorio, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task <ResponseDespesaJson> Execute(RequestDispesaJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();
         
        var despesa = _mapper.Map<Dispesa>(request);
        despesa.UserId = loggedUser.Id;

        await _repositorio.add(despesa);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseDespesaJson>(despesa);
    }
        
    private void Validate(RequestDispesaJson request)
    {
        var validator = new DespensasValidator();    

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
