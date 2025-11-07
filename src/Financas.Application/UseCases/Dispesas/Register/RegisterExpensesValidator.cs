using Financas.Communication.Request;
using FluentValidation;

namespace Financas.Application.UseCases.Dispesas.Register;
public class RegisterExpensesValidator : AbstractValidator<RequestDispesaJson>
{
    public RegisterExpensesValidator()
    {
        RuleFor(Dispesas => Dispesas.Titulo).NotEmpty().WithMessage("O título da dispesa é obrigatório.");
        RuleFor(Dispesas => Dispesas.Valor).GreaterThan(0).WithMessage("O valor da dispesa deve ser maior que zero.");
        RuleFor(Dispesas => Dispesas.Data).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data da dispesa não pode ser futura.");
        RuleFor(Dispesas => Dispesas.Pagamento).IsInEnum().WithMessage("O tipo de pagamento da dispesa é inválido.");
    }
}