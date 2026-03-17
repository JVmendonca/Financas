using Financas.Communication.Request;
using FluentValidation;

namespace Financas.Application.UseCases.User.Password;
public class UpdatePasswordValidator : AbstractValidator<RequestUpdatePasswordJson>
{
    public UpdatePasswordValidator()
    {
        RuleFor(x => x.NovaSenha).SetValidator(new SenhaValidator<RequestUpdatePasswordJson>());
    }
}
