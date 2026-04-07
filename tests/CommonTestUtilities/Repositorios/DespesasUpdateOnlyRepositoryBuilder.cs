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

    public DespesasUpdateOnlyRepositoryBuilder GetById(User user, Despesa? despesa)
    {
        if (despesa is not null)
            _repository.Setup(r => r.GetById(user, despesa.Id)).ReturnsAsync(despesa);

        return this;
    }

    public IDespesasUpdateOnlyRepositorio Build()
    {
        return _repository.Object;
    }
}
