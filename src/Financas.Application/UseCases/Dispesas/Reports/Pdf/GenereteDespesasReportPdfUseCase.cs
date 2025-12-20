using Financas.Application.UseCases.Dispesas.Reports.Pdf.Colors;
using Financas.Application.UseCases.Dispesas.Reports.Pdf.Fonts;
using Financas.Domain.Extensions;
using Financas.Domain.Reports;
using Financas.Domain.Repositorios.Despesas;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace Financas.Application.UseCases.Dispesas.Reports.Pdf;
public class GenereteDespesasReportPdfUseCase : IGenereteDespesasReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_DESPESA = 25;
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

        var document = CreateDocument(mes);
        var page = CreatePage(document);


        CreateHeaderProfilePhotoAndName(page);

        var total_despesas = despesas.Sum(despesa => despesa.Valor);
        CreateTotalSpentSection(page, mes, total_despesas);

        foreach(var despesa in despesas)
        {
            var table = CreateDespesaTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_DESPESA;

            AddDespesasTitulo(row.Cells[0], despesa.Titulo);

            AddHeaderForAmout(row.Cells[3]);


            row = table.AddRow();
            row.Height = HEIGHT_ROW_DESPESA;

            row.Cells[0].AddParagraph(despesa.Data.ToString("D"));
            SetStyleForDespesaInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(despesa.Data.ToString("t"));
            SetStyleForDespesaInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(despesa.Pagamento.PaymentTypeToString());
            SetStyleForDespesaInformation(row.Cells[2]);
            AddAmoutForDespesa(row.Cells[3], despesa.Valor);
            
            if(string.IsNullOrEmpty(despesa.Descricao) == false)
            {
                var descricaoRow = table.AddRow();
                descricaoRow.Height = HEIGHT_ROW_DESPESA;

                descricaoRow.Cells[0].AddParagraph(despesa.Descricao);
   
                descricaoRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descricaoRow.Cells[0].Shading.Color = ColorsHelper.GREEM_LIGHT;
                descricaoRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descricaoRow.Cells[0].MergeRight = 2;
                descricaoRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        return RenderDocuments(document);
    }


    private Document CreateDocument(DateOnly mes)
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

    private Section CreatePage(Document document)
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

    private void CreateHeaderProfilePhotoAndName(Section page) 
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("250");


        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "Joaovtm.png");

        var image = row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Olá, João Vitor");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly mes, decimal total_despesas)
    {
        var paragrafo = page.AddParagraph();
        paragrafo.Format.SpaceBefore = "40";
        paragrafo.Format.SpaceAfter = "40";

        var titulo = string.Format(ResourceReportGenerationMessages.TOTAL_GASTO_EM, mes.ToString("Y"));

        paragrafo.AddFormattedText(titulo, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragrafo.AddLineBreak();

        
        paragrafo.AddFormattedText($"{total_despesas} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateDespesaTable(Section page)
    { 
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;   
    }

    private void AddDespesasTitulo(Cell cell, string despesaTitulo)
    {
        cell.AddParagraph(despesaTitulo);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITHE };
        cell.Shading.Color = ColorsHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForAmout(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.VALOR);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleForDespesaInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEM_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }   

    private void AddAmoutForDespesa(Cell cell, decimal valor)
    {
        cell.AddParagraph($" - {CURRENCY_SYMBOL} {valor}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEM_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
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
