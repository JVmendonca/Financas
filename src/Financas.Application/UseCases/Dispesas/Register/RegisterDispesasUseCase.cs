using Financas.Communication.Enuns;
using Financas.Communication.Request;
using Financas.Communication.Responses;

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
        var validator = new RegisterExpensesValidator();    

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ArgumentException();
        }
    }

}
