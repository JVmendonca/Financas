using Financas.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    private readonly Mock<IPassowordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPassowordEncripter>();

        _mock.Setup(PasswordEncripter => PasswordEncripter.Encrypt(It.IsAny<string>())).Returns(("24083066Jj*"));
    }

    public PasswordEncripterBuilder verify(string? password)
    {

        if (string.IsNullOrWhiteSpace(password))
        {
            _mock.Setup(PasswordEncripter => PasswordEncripter.Verify(password, It.IsAny<string>())).Returns(true);
        }
       

        return this;
    }

    public IPassowordEncripter Build() => _mock.Object;
}
