using Financas.Domain.Security.Cryptography;

namespace Financas.Infrasctructure.Security.Cryptography;
internal class Crytptography : IPassowordEncripter
{
    public string Encrypt(string senha)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(senha);


        return passwordHash;
    }


    public bool Verify(string senha, string senhaHash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
    }
}
