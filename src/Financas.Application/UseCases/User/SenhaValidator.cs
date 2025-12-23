using Financas.Exeption;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Financas.Application.UseCases.User;
public class SenhaValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage.";  

    public override string Name => "SenhaValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{ErrorMessage}";
    }

    public override bool IsValid(ValidationContext<T> context, string senha)
    {
        if (string.IsNullOrEmpty(senha))
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMassages.SENHA_INVALIDA );
            return false;
        }

        if (senha.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMassages.SENHA_INVALIDA );
            return false;
        }

        if (Regex.IsMatch(senha, @"[A-Z]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMassages.SENHA_INVALIDA);
            return false;
        }
        if (Regex.IsMatch(senha, @"[a-z]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMassages.SENHA_INVALIDA);
            return false;
        }
        if (Regex.IsMatch(senha, @"[0-9]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMassages.SENHA_INVALIDA);
            return false;
        }
        if (Regex.IsMatch(senha, @"[\!\@\#\$\*\.]+") == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMassages.SENHA_INVALIDA);
            return false;
        }

        return true;
    }
}
