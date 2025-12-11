using Financas.Domain.Repositorios.Despesas
namespace Financas.Application.UseCases.Dispesas.Reports.Pdf;
public class GenereteDespesasReportPdfUseCase : IGenereteDespesasReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IDespesasReadOnlyRepositorio _repositorio;

    public GenereteDespesasReportPdfUseCase(IDespesasReadOnlyRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<byte[]> Execute(DateOnly mes)
    {
        var despesas = await _repositorio.FilterByMonth(mes);
        if (despesas.Count == 0)
        {
            return [];
        }


        return [];
    }
}
