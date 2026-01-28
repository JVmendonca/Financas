using Financas.Domain.Repositorios.Despesas;
using Moq;

namespace CommonTestUtilities.Repositorios;
public class DespesasWriteOnlyRepositorioBuilder
{
    public static IDespesasWriteOnlyRepositorio Build()
    {
        var mock = new Mock<IDespesasWriteOnlyRepositorio>();

        return mock.Object;
    }
}
