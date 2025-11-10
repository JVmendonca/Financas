using System.Globalization;

namespace Financas.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        var supportdLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestdCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("pt-BR");

        if(string.IsNullOrWhiteSpace(requestdCulture) == false 
            && supportdLanguages.Exists(laguage => laguage.Name.Equals(requestdCulture)))
        {
            cultureInfo = new CultureInfo(requestdCulture);

        }
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);

    }
}
