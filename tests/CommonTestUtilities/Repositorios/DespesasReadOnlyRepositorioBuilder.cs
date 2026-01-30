using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Despesas;
using Moq;

namespace CommonTestUtilities.Repositorios;
public class DespesasReadOnlyRepositorioBuilder
{
    private readonly Mock<IDespesasReadOnlyRepositorio> _repository;

    public DespesasReadOnlyRepositorioBuilder()
    {
        _repository = new Mock<IDespesasReadOnlyRepositorio>();
    }

    public DespesasReadOnlyRepositorioBuilder GetAll(User user, List<Dispesa> dispesas)
    {
        _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(dispesas);

        return this;
    }

    public IDespesasReadOnlyRepositorio Build() => _repository.Object;
}
