using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositorios;
using CommonTestUtilities.Requests;
using Financas.Application.UseCases.User.Update;
using Financas.Domain.Entidades;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace Validator.Tests.Users.Update;
public class UpdateUserValidatorTest
{
    [Fact]
    public async Task success()
    {
        var user = UserBuild.Build();
        var request = RequestUpdateUserJsonBuilder.Build();

        var userCase = CreatUseCase(user);

        var act = async () => await userCase.Execute(request);

        await act.Should().NotThrowAsync();

        user.Nome.Should().Be(request.Nome);
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task Error_Nome_Empty()
    {
        var user = UserBuild.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Nome = string.Empty;

        var userCase = CreatUseCase(user);

        var act = async () => await userCase.Execute(request);
        
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.NOME_VAZIO));

    }

    [Fact]
    public async Task Error_Email_Not_Exist()
    {
        var user = UserBuild.Build();
        var request = RequestUpdateUserJsonBuilder.Build();

        var userCase = CreatUseCase(user, request.Email);

        var act = async () => await userCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.EMAIL_JA_EM_USO));
    }

    private UpdateUserUserCase CreatUseCase(User user, string? email = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var UpdateRepository = UserUpdateOnlyRepositoryBuild.Build(user);
        var loggedUser = LoggedUserBuilder.Build(user);
        var readRepository = new UserReadOlnlyRepositorioBuider();

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistsByEmail(email);
        }
        return new UpdateUserUserCase(loggedUser, UpdateRepository, readRepository.Build(), unitOfWork);
    }
}
