using Financas.Domain.Entidades;

namespace WebApi.Test.Resorces;
public class DespesasIndentifyManeger
{
    private readonly Despesa _despesa;

    public DespesasIndentifyManeger(Despesa despesa)
    {
        _despesa = despesa;
    }

    public long GetId() => _despesa.Id;
    public DateTime GetDate() => _despesa.Data;
}
