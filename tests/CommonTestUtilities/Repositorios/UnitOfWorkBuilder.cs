using Financas.Domain.Repositorios;

namespace CommonTestUtilities.Repositorios;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Moq.Mock<IUnitOfWork>();
         
        return mock.Object;
    }

}
