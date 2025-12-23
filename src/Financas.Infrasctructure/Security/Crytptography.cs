using Financas.Domain.Security.Cryptography;

namespace Financas.Infrasctructure.Security;
internal class Crytptography : IPassowordEncripter
{
    public string Encript(string senha)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(senha);

        return passwordHash;
    }
}
