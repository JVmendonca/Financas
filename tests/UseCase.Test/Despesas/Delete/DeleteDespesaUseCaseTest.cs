using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositorios;
using Financas.Application.UseCases.Dispesas.Delete;
using Financas.Domain.Entidades;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace UseCase.Test.Despesas.Delete;
public class DeleteDespesaUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuild.Build();
        var despesa = DespesasBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, despesa);

        var act = async () => await useCase.Execute(despesa.Id);

        await act.Should().NotThrowAsync();

    }
    [Fact]
    public async Task Error_Not_Foud()
    {
        var loggedUser = UserBuild.Build();
        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundExeption>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA));
    }

    private DeleteDespesaUseCase CreateUseCase(User user, Dispesa dispesa = null)
    {
        var repositoryWriteOnly = DespesasWriteOnlyRepositorioBuilder.Build();
        var repository = new DespesasReadOnlyRepositorioBuilder().GetById(user, dispesa).Build();
        var unitOfwor = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new DeleteDespesaUseCase(repositoryWriteOnly, unitOfwor, loggedUser, repository);
    }
}
