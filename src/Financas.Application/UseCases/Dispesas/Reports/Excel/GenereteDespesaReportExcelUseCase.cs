using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Financas.Application.UseCases.Dispesas.Reports.Excel;
public class GenereteDespesaReportExcelUseCase : IGenereteDespesaReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly mes)
    {
        var workbook = new XLWorkbook();

        workbook.Author = "Joao Vitor";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.AddWorksheet(mes.ToString("MM-yyyy"));
    }
}
