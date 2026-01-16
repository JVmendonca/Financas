using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.User;
using Moq;

namespace CommonTestUtilities.Repositorios;
public class UserReadOlnlyRepositorioBuider
{
    private readonly Moq.Mock<IUserReadOnlyRepository> _repository;

    public UserReadOlnlyRepositorioBuider()
    {
        _repository = new Moq.Mock<IUserReadOnlyRepository>();
    }

    public void ExistsByEmail(string email)
    {
        _repository.Setup(userReadonly => userReadonly.ExistsByEmail(email)).ReturnsAsync(true);
    }

    public UserReadOlnlyRepositorioBuider GetUSerByEmail(User user)
    {
        _repository.Setup(userReadonly => userReadonly.GetUserByEmai(user.Email)).ReturnsAsync(user);

        return this;
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}
