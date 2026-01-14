using CommonTestUtilities.Requests;
using Financas.Application.UseCases.Dispesas;
using Financas.Exeption;
using FluentAssertions;
namespace Validator.Tests.Dispesas.Register;
public class RegisterDispensasValidatorTest
{
    [Fact]
    public void success()
    {
        // Preparar
        var validator = new DespensasValidator();
        var request = RequestDispesaJsonBuilder.Build();

        // Agir
        var reuslt = validator.Validate(request);

        // Valida
        reuslt.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Titulo_Obrigatorio()
    {
        // Preparar
        var validator = new DespensasValidator();
        var request = RequestDispesaJsonBuilder.Build();
        request.Titulo = string.Empty;
        //request.Valor = 0;

        // Agir
        var reuslt = validator.Validate(request);

        // Valida
        reuslt.IsValid.Should().BeFalse();
        reuslt.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMassages.TITULO_OBRIGATORIO));
        
    }

    [Fact]
    public void Error_Data_Futura()
    {
        // Preparar
        var validator = new DespensasValidator();
        var request = RequestDispesaJsonBuilder.Build();
        request.Data = DateTime.UtcNow.AddDays(1);
        //request.Valor = 0;

        // Agir
        var reuslt = validator.Validate(request);

        // Valida
        reuslt.IsValid.Should().BeFalse();
        reuslt.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMassages.DATA_NAO_DEVE_SER_FUTURA));
        
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-3)]
    public void Error_Valor_Invalido(decimal valor)
    {
        // Preparar
        var validator = new DespensasValidator();
        var request = RequestDispesaJsonBuilder.Build();
        request.Valor = valor;
        // Agir
        var reuslt = validator.Validate(request);

        // Valida
        reuslt.IsValid.Should().BeFalse();
        reuslt.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMassages.VALOR_DEVE_SER_MAIOR_ZERO));
        
    }

}
