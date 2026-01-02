using Financas.Domain.Entidades;
using Financas.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Financas.Infrasctructure.Security.Tokens;
internal class JwtTokenGenerator : IAccesTokenGeneretor
{
    private readonly uint _expirationTimeMintes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationTimeMintes, string signingKey)
    {
        _expirationTimeMintes = expirationTimeMintes;
        _signingKey = signingKey;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Sid, user.UserIndetificador.ToString())
            
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMintes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity()
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);

    }


    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}
