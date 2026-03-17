using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.User;
using Moq;

namespace CommonTestUtilities.Repositorios;
public class UserUpdateOnlyRepositoryBuild
{
    public static IUserUpdateOnlyRepository Build(User user)
    {
        var mok = new Mock<IUserUpdateOnlyRepository>();

        mok.Setup(repository => repository.GetById(user.Id)).ReturnsAsync(user);

        return mok.Object;
    }
}
