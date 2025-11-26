namespace Financas.Communication.Responses;
public class ResponseShortExpenseJson
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public Decimal Valor { get; set; }

}
