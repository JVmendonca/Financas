using Financas.Domain.Repositorios.User;

namespace CommonTestUtilities.Repositorios;
public class UserReadOlnlyRepositorioBuider
{
    private readonly Moq.Mock<IUserReadOnlyRepository> _repository;

    public UserReadOlnlyRepositorioBuider()
    {
        _repository = new Moq.Mock<IUserReadOnlyRepository>();
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}
