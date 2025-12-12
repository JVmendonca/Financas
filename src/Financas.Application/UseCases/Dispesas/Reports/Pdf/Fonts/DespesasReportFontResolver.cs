using DocumentFormat.OpenXml.Presentation;
using PdfSharp.Fonts;
using System.Reflection;

namespace Financas.Application.UseCases.Dispesas.Reports.Pdf.Fonts;
public class DespesasReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        if (stream == null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }

        var tamanho = (int)stream!.Length;

        var data = new byte[tamanho];

        stream.Read(data, 0, tamanho);

        return data;

    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"Financas.Application.UseCases.Dispesas.Reports.Pdf.Fonts.{faceName}.ttf");
    }
}
