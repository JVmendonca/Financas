using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositorios;
using Financas.Application.UseCases.Despesas.Reports.Excel;
using Financas.Domain.Entidades;
using FluentAssertions;

namespace UseCase.Test.Despesas.Reports.Excel;
public class GenerateDespesasReportExcelUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuild.Build();
        var despesa = DespesasBuilder.Collection(loggedUser);
        
        var useCase = CreateUseCase(loggedUser, despesa);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }
    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuild.Build();

        var useCase = CreateUseCase(loggedUser, []);

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    private GenereteDespesaReportExcelUseCase CreateUseCase(User user, List<Despesa> despesas)
    {
        var repository = new DespesasReadOnlyRepositorioBuilder().FilterByMonth(user, despesas).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenereteDespesaReportExcelUseCase(repository,loggedUser);
    }
}
