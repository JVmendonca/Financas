using Financas.Domain.Enuns;

namespace Financas.Domain.Enums.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            Financas.Domain.Enuns.PaymentType.dinheiro => "Dinheiro",
            Financas.Domain.Enuns.PaymentType.cartao_credito => "Cartão de Crédito",
            Financas.Domain.Enuns.PaymentType.cartao_debito => "Cartão de Débito",
            Financas.Domain.Enuns.PaymentType.pix => "Pix",
            _ => string.Empty
        };
    }
}
