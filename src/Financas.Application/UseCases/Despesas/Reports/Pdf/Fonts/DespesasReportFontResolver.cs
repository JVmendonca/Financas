using PdfSharp.Fonts;
using System.Reflection;

namespace Financas.Application.UseCases.Despesas.Reports.Pdf.Fonts;
public class DespesasReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName)
                     ?? ReadFontFile(FontHelper.DEFAULT_FONT);

        if (stream == null)
        {
            // decida o comportamento: null, exception clara, etc.
            throw new InvalidOperationException(
                $"Não foi possível carregar a fonte '{faceName}' nem a fonte padrão '{FontHelper.DEFAULT_FONT}'. " +
                "Verifique se os arquivos .ttf estão marcados como Embedded Resource e com o namespace correto.");
        }

        var tamanho = (int)stream.Length;
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
        var resourceName =
            $"Financas.Application.UseCases.Despesas.Reports.Pdf.Fonts.{faceName}.ttf";

        return assembly.GetManifestResourceStream(resourceName);
    }
}
