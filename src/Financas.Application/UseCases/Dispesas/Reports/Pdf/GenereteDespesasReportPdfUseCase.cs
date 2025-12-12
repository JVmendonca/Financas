using Financas.Application.UseCases.Dispesas.Reports.Pdf.Fonts;
using Financas.Domain.Reports;
using Financas.Domain.Repositorios.Despesas;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace Financas.Application.UseCases.Dispesas.Reports.Pdf;
public class GenereteDespesasReportPdfUseCase : IGenereteDespesasReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IDespesasReadOnlyRepositorio _repositorio;

    public GenereteDespesasReportPdfUseCase(IDespesasReadOnlyRepositorio repositorio)
    {
        _repositorio = repositorio;

        GlobalFontSettings.FontResolver = new DespesasReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly mes)
    {
        var despesas = await _repositorio.FilterByMonth(mes);
        if (despesas.Count == 0)
        {
            return [];
        }

        var document = CreatDocument(mes);
        var page = CreatPage(document);

        var paragrafo = page.AddParagraph();
        var titulo = string.Format(ResourceReportGenerationMessages.TOTAL_GASTO_EM, mes.ToString("Y"));

        paragrafo.AddFormattedText(titulo, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragrafo.AddLineBreak();

        var total_despesas = despesas.Sum(despesa => despesa.Valor);
        paragrafo.AddFormattedText($"{total_despesas} {CURRENCY_SYMBOL}", new Font {Name = FontHelper.WORKSANS_BlACK, Size = 50});

        return RenderDocuments(document);
    }


    private Document CreatDocument(DateOnly mes)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessages.DESPESA_PARA} {mes:Y}";
        document.Info.Author = "João Vitor";

        var style = document.Styles["Normal"];
        if (style != null)
        {
            style.Font.Name = FontHelper.DEFAULT_FONT;
        }

        return document;
    }

    private Section CreatPage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private byte[] RenderDocuments(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);
        
        return file.ToArray();
    }
}
