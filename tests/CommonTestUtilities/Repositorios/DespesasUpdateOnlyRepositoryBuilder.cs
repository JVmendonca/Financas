using Financas.Domain.Entidades;
using Financas.Domain.Repositorios.Despesas;
using Moq;

namespace CommonTestUtilities.Repositorios;
public class DespesasUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IDespesasUpdateOnlyRepositorio> _repository;

    public DespesasUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IDespesasUpdateOnlyRepositorio>();
    }

    public DespesasUpdateOnlyRepositoryBuilder GetById(User user, Dispesa? dispesa)
    {
        if (dispesa is not null)
            _repository.Setup(r => r.GetById(user, dispesa.Id)).ReturnsAsync(dispesa);

        return this;
    }

    public IDespesasUpdateOnlyRepositorio Build()
    {
        return _repository.Object;
    }
}
