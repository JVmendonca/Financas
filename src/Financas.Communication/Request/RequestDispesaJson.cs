using Financas.Communication.Enuns;

namespace Financas.Communication.Request;
public class RequestDispesaJson
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set;}
    public decimal Valor { get; set; }
    public PaymentType Pagamento { get; set; }
}
