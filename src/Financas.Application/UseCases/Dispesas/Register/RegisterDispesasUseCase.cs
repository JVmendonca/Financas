using Financas.Communication.Enuns;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Domain.Entidades;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterDispesasUseCase
{
    public ResponseDispesaJson Execute(RequestDispesaJson request)
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
