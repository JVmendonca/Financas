namespace Financas.Application.UseCases.Dispesas.Reports.Excel;
public interface IGenereteDespesaReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly mes);
}
