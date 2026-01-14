using Financas.Domain.Repositorios;
using Financas.Domain.Security.Tokens;
using Moq;

namespace CommonTestUtilities.Repositorios;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Moq.Mock<IUnitOfWork>();
         
        return mock.Object;
    }

}
