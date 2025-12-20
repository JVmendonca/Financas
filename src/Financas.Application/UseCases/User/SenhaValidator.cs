using FluentValidation;
using FluentValidation.Validators;

namespace Financas.Application.UseCases.User;
public class SenhaValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "SenhaValidator";

    public override bool IsValid(ValidationContext<T> context, string senha)
    {

    }
}
