using Financas.Communication.Enuns;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterDispesasUseCase
{
    public ResponseDispesaJson Execute(RequestDispesaJson request)
    {
        Validate(request);

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
