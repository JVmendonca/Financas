using Financas.Domain.Repositorios.User;

namespace CommonTestUtilities.Repositorios;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Moq.Mock<IUserWriteOnlyRepository>();
        return mock.Object;
    }
}
