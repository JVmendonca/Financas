using System.ComponentModel.DataAnnotations;
using CommonTestUtilities;
using Financas.Application.UseCases.Dispesas.Register;
using Financas.Communication.Request;
using FluentAssertions;
namespace Validator.Tests.Dispesas.Register;
public class RegisterDispensasValidatorTest
{
    [Fact]
    public void success()
    {
        // Preparar
        var validator = new RegisterDispensasValidator();
        var request = RequestDispesaJsonBuilder.Build();

        // Agir
        var reuslt = validator.Validate(request);

        // Valida
        reuslt.IsValid.Should().BeTrue();
    }
    
}
