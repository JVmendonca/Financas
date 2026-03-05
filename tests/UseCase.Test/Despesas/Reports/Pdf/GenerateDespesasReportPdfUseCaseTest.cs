using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositorios;
using Financas.Application.UseCases.Dispesas.Reports.Excel;
using Financas.Domain.Entidades;
using FluentAssertions;

namespace UseCase.Test.Despesas.Reports.Pdf;
public class GenerateDespesasReportPdfUseCaseTest
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

        var useCase = CreateUseCase(loggedUser, new List<Dispesa>());

        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();
    }

    private GenereteDespesaReportExcelUseCase CreateUseCase(User user, List<Dispesa> dispesas)
    {
        var repository = new DespesasReadOnlyRepositorioBuilder().FilterByMonth(user, dispesas).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenereteDespesaReportExcelUseCase(repository, loggedUser);
    }
}
