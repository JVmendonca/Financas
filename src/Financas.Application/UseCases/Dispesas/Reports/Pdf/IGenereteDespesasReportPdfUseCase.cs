namespace Financas.Application.UseCases.Dispesas.Reports.Pdf;
public interface IGenereteDespesasReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly mes);
}
