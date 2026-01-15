using Financas.Domain.Entidades;
using Financas.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Token;
public class JJwtTokenGeneratorBuilder
{
    public static IAccesTokenGeneretor Build()
    {
        var mock = new Moq.Mock<IAccesTokenGeneretor>();

        mock.Setup(config => config.Generate(It.IsAny<User>())).Returns("token");

        return mock.Object;
    }
}
