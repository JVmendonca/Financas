namespace Financas.Application.UseCases.Despesas.Reports.Excel;
public interface IGenereteDespesaReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly mes);
}
