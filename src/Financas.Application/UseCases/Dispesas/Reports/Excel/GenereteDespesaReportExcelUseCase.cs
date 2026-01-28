using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Financas.Communication.Enuns;
using Financas.Domain.Entidades;
using Financas.Domain.Extensions;
using Financas.Domain.Reports;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Services.LoggedUser;

namespace Financas.Application.UseCases.Dispesas.Reports.Excel;
public class GenereteDespesaReportExcelUseCase : IGenereteDespesaReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";

    private readonly IDespesasReadOnlyRepositorio _repositorio;
    private readonly ILoggedUser _loggedUser;

    public GenereteDespesaReportExcelUseCase(IDespesasReadOnlyRepositorio repositorio, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _loggedUser = loggedUser;
    }

    public async Task<byte[]> Execute(DateOnly mes)
    {
        var loggedUser = await _loggedUser.Get();

        var despesas = await _repositorio.FilterByMonth(loggedUser,mes);
        if (despesas.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Author = loggedUser.Nome;
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.AddWorksheet(mes.ToString("MM-yyyy"));

        InsertHeader(worksheet);

        var raw = 2;
        foreach (var despesa in despesas)
        {
            worksheet.Cell($"A{raw}").Value = despesa.Titulo;

            // ⬇ Corrigido: escrever como DateTime e aplicar formato adequado
            var date = new DateTime(despesa.Data.Year, despesa.Data.Month, despesa.Data.Day);
            worksheet.Cell($"B{raw}").Value = date;
            worksheet.Cell($"B{raw}").Style.DateFormat.Format = "dd/MM/yyyy"; // evita #####

            worksheet.Cell($"C{raw}").Value = despesa.Pagamento.PaymentTypeToString();

            worksheet.Cell($"D{raw}").Value = despesa.Valor;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #, ##0.00";

            worksheet.Cell($"E{raw}").Value = despesa.Descricao;

            raw++;
        }

        // ⬇ Ajuste padrão + garantir largura mínima da coluna de datas (correção definitiva)
        worksheet.Columns().AdjustToContents();
        worksheet.Column(2).Width = Math.Max(worksheet.Column(2).Width, 12); // força largura adequada

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }


    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITULO;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATA;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAGAMENTO;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.VALOR;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRICAO;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
