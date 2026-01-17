using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entites;
using CommonTestUtilities.Repositorios;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using Financas.Application.UseCases.Login;
using Financas.Domain.Entidades;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace UseCase.Test.Users.Login;
public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuild.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreatUseCase(user, request.Senha);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Nome.Should().Be(user.Nome);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_User_Not_Match()
    {
        var user = UserBuild.Build();
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreatUseCase(user, request.Senha);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InavalidLoginExeception>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.EMAIL_OU_SENHA_ERRADO));
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        var user = UserBuild.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreatUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InavalidLoginExeception>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.EMAIL_OU_SENHA_ERRADO));
    }

    private DoLoginUseCase CreatUseCase(Financas.Domain.Entidades.User user, string? password = null)
    {
        var passwordEncripter = new PasswordEncripterBuilder().verify(password).Build();
        var TokenGenerator = JJwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOlnlyRepositorioBuider().GetUSerByEmail(user).Build();

        return new DoLoginUseCase(readRepository, passwordEncripter, TokenGenerator);
    }
}
