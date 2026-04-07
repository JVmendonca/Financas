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

    public DespesasReadOnlyRepositorioBuilder GetAll(User user, List<Despesa> despesas)
    {
        _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(despesas);

        return this;
    }

    public DespesasReadOnlyRepositorioBuilder GetById(User user, Despesa? despesa)
    {
        if (despesa is not null)
            _repository.Setup(repository => repository.GetById(user, despesa.Id)).ReturnsAsync(despesa);

        return this;
    }

    public DespesasReadOnlyRepositorioBuilder FilterByMonth(User user, List<Despesa> despesas)
    {
        _repository.Setup(repository => repository.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(despesas);
        return this;
    }

    public IDespesasReadOnlyRepositorio Build() => _repository.Object;
}
