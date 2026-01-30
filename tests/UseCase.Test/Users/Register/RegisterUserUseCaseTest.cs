using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorios;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using Financas.Application.UseCases.User.Register;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace UseCase.Test.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Nome.Should().Be(request.Nome);
        result.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Nome_Empty()
    {
        var useCase = CreateUseCase();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Nome = string.Empty;


        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.NOME_VAZIO));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = new PasswordEncripterBuilder().Build();
        var WriteRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var TokenGeneretor = JJwtTokenGeneratorBuilder.Build();
        var readRepository = new UserReadOlnlyRepositorioBuider();

        if(string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistsByEmail(email);
        }

        return new RegisterUserUseCase(mapper, passwordEncripter, readRepository.Build(), WriteRepository, unitOfWork, TokenGeneretor);
    }
}
