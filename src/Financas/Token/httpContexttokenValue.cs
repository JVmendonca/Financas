
using Financas.Domain.Security.Tokens;

namespace Financas.Token;

public class httpContexttokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public httpContexttokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }

    public string TokenOnRequest()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        var token = authorization["Bearer ".Length..].Trim();

        return token;
    }
}
