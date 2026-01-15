using Financas.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPassowordEncripter Build()
    {
        var mock = new Moq.Mock<IPassowordEncripter>();

        mock.Setup(PasswordEncripter => PasswordEncripter.Encrypt(It.IsAny<string>())).Returns("SenhaEncriptada");

        return mock.Object;

    }
}
