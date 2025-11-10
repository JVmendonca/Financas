using System.ComponentModel.DataAnnotations;
using Financas.Application.UseCases.Dispesas.Register;
using Financas.Communication.Request;
namespace Validator.Tests.Dispesas.Register;
public class RegisterDispensasValidatorTest
{
    [Fact]
    public void success()
    {
        // Preparar
        var validator = new RegisterDispensasValidator();
        var request = new RequestDispesaJson
        {
            Titulo = "Aluguel",
            Descricao = "Pagamento do aluguel de junho",
            Valor = 1500.00m,
            Data = DateTime.UtcNow.AddDays(-1),
            Pagamento = Financas.Communication.Enuns.PaymentType.pix
        };
        // Agir
        var reuslt = validator.Validate(request);

        // Valida
        Assert.True(reuslt.IsValid); 
    }


}
