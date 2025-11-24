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
    public RegisterDispesasUseCase(IDispesasRepositorio repositorio, IUnitOfWork unitOfWork)
    {
        _repositorio = repositorio;
        _unitOfWork = unitOfWork;
    }
    public async Task <ResponseDispesaJson> Execute(RequestDispesaJson request)
    {
        Validate(request);

        var entity = new Dispesa
        {
            Titulo = request.Titulo,
            Descricao = request.Descricao,
            Data = request.Data,
            Valor = request.Valor,
            Pagamento = (Domain.Enuns.PaymentType)request.Pagamento
        };

        await _repositorio.add(entity);

        await _unitOfWork.Commit();

        return new ResponseDispesaJson();
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
