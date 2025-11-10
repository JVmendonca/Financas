using Financas.Communication.Request;
using FluentValidation;
using Financas.Exeption;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterDispensasValidator : AbstractValidator<RequestDispesaJson>
{

    public RegisterDispensasValidator()
    {
        RuleFor(Dispesas => Dispesas.Titulo).NotEmpty().WithMessage(ResourceErrorMassages.TITULO_OBRIGATORIO);
        RuleFor(Dispesas => Dispesas.Valor).GreaterThan(0).WithMessage(ResourceErrorMassages.VALOR_DEVE_SER_MAIOR_ZERO);
        RuleFor(Dispesas => Dispesas.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMassages.DATA_NAO_DEVE_SER_FUTURA);
        RuleFor(Dispesas => Dispesas.Pagamento).IsInEnum().WithMessage(ResourceErrorMassages.TIPO_PAGAMENTO_INVALIDO);
    }
}