namespace Financas.Domain.Entidades;
public class Tag
{
    public long Id { get; set; }

    public Enums.Tag Value { get; set; }

    public long DespesaId { get; set; }

    public Despesa Despesa { get; set; } = default!;
}
