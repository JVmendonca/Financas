namespace Financas.Domain.Security.Cryptography;
public interface IPassowordEncripter
{
    string Encrypt(string senha);
    bool Verify(string senha, string senhaHash);
}
