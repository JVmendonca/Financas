using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Financas.Domain.Reports;

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

        InsertHeader(worksheet);
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourcereportGenerationMessages.TITULO;
        worksheet.Cell("B1").Value = ResourcereportGenerationMessages.DATA;
        worksheet.Cell("C1").Value = ResourcereportGenerationMessages.PAGAMENTO;
        worksheet.Cell("D1").Value = ResourcereportGenerationMessages.VALOR;
        worksheet.Cell("E1").Value = ResourcereportGenerationMessages.DESCRICAO;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }

}
