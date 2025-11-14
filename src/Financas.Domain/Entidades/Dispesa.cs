using Financas.Domain.Enuns;

namespace Financas.Domain.Entidades;
public class Dispesa
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public PaymentType Pagamento { get; set; }  
}