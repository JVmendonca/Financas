using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorios;
using Financas.Application.UseCases.Dispesas.GetAll;
using Financas.Domain.Entidades;
using FluentAssertions;

namespace WebApi.Test.Despesas.GetAll;
public class GetAllDespesasUseCaseTest
{
    [Fact]
    public async Task Succes()
    {
        var loggedUser = UserBuild.Build();
        var despesas = DespesasBuilder.Collection(loggedUser);

        var userCase = CreatUseCase(loggedUser, despesas);

        var result = await userCase.Execute();

        result.Should().NotBeNull();
        result.Despesas.Should().NotBeNullOrEmpty().And.AllSatisfy(despesa =>
        {
            despesa.Id.Should().BeGreaterThan(0);
            despesa.Titulo.Should().NotBeNullOrEmpty();
            despesa.Valor.Should().BeGreaterThan(0);
        });
    }

    private GetAllExpensesUseCase CreatUseCase(User user, List<Dispesa> dispesas)
    {
        var repositorio = new DespesasReadOnlyRepositorioBuilder().GetAll(user, dispesas).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetAllExpensesUseCase(repositorio, mapper, loggedUser);
    }
}
