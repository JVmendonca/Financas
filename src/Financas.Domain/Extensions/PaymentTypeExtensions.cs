using Financas.Domain.Enuns;

namespace Financas.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.dinheiro => "Dinheiro",
            PaymentType.cartao_credito => "Cartão de Crédito",
            PaymentType.cartao_debito => "Cartão de Débito",
            PaymentType.pix => "Pix",
            _ => string.Empty
        };
    }
}
