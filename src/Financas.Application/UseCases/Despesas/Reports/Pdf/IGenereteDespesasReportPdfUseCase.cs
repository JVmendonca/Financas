namespace Financas.Application.UseCases.Despesas.Reports.Pdf;
public interface IGenereteDespesasReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly mes);
}
