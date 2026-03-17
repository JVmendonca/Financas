using Financas.Communication.Request;
using Financas.Exeption;
using FluentValidation;

namespace Financas.Application.UseCases.User.Update;
public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(ResourceErrorMassages.NOME_VAZIO);
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMassages.EMAIL_VAZIO);

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(ResourceErrorMassages.EMAIL_INVALIDO)
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
