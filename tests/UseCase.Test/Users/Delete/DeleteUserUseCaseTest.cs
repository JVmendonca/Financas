using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositorios;
using Financas.Application.UseCases.User.Delete;
using Financas.Domain.Entidades;
using FluentAssertions;

namespace UseCase.Test.Users.Delete;
public class DeleteUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuild.Build();
        var useCase = CreatUseCase(user);

        var act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }

    private DeleteProfileUseCase CreatUseCase(User user)
    {
        var repository = UserWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteProfileUseCase(unitOfWork,repository ,loggedUser );
    }
}
