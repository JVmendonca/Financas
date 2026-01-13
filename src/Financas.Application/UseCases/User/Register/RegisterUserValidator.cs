using Financas.Communication.Request;
using Financas.Exeption;
using FluentValidation;


namespace Financas.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceErrorMassages.NOME_VAZIO);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMassages.EMAIL_VAZIO)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMassages.EMAIL_INVALIDO);

        RuleFor(user => user.Senha).SetValidator(new SenhaValidator<RequestRegisterUserJson>());
    }
}
