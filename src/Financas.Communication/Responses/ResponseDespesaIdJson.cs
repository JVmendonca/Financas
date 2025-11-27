using Financas.Communication.Enuns;

namespace Financas.Communication.Responses;
public class ResponseDespesaIdJson
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime Date { get; set; }
    public decimal Valor { get; set; }
    public PaymentType PaymentType { get; set; }
}
