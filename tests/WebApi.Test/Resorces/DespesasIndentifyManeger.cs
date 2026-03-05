using Financas.Domain.Entidades;

namespace WebApi.Test.Resorces;
public class DespesasIndentifyManeger
{
    private readonly Dispesa _despesa;

    public DespesasIndentifyManeger(Dispesa dispesa)
    {
        _despesa = dispesa;
    }

    public long GetId() => _despesa.Id;
    public DateTime GetDate() => _despesa.Data;
}
