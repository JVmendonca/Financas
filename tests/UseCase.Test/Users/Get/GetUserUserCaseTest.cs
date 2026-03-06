using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using Financas.Application.UseCases.User.Get;
using Financas.Domain.Entidades;
using FluentAssertions;

namespace UseCase.Test.Users.Get;
public class GetUserUserCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuild.Build();
        var useCase = CreatUseCase(user);

        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Nome.Should().Be(user.Nome);
        result.Email.Should().Be(user.Email);
    }

    private GetUserUseCase CreatUseCase(User user)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetUserUseCase(loggedUser, mapper);
    }
}
