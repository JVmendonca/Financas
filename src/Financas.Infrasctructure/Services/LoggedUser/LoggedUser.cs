using Financas.Domain.Entidades;
using Financas.Domain.Security.Tokens;
using Financas.Domain.Services.LoggedUser;
using Financas.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Financas.Infrasctructure.Services;
public class LoggedUser : ILoggedUser
{
    private readonly FinancasDbContexto _dbcontext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(FinancasDbContexto dbcontext, ITokenProvider tokenProvider)
    {
        _dbcontext = dbcontext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var indetifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;



        return await _dbcontext
            .Users.AsNoTracking()
            .FirstAsync(user => user.UserIndetificador == Guid.Parse(indetifier));
    }
}
