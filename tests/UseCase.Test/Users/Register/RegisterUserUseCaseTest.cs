using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorios;
using CommonTestUtilities.Requests;
using Financas.Application.UseCases.User.Register;
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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWorkBuilde = UnitOfWorkBuilder.Build();
        var WriteRepository = UserWriteOnlyRepositoryBuilder.Build();

        return new RegisterUserUseCase(mapper, null, null, WriteRepository, null, unitOfWorkBuilde);
    }
}
