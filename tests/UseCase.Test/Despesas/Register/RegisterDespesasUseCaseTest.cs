using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorios;
using CommonTestUtilities.Requests;
using Financas.Application.UseCases.Dispesas.Register;
using Financas.Domain.Repositorios.Despesas;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace UseCase.Test.Despesas.Register;
public class RegisterDespesasUseCaseTest
{
    [Fact]
    public async Task Success() 
    {
        var loggedUser = UserBuild.Build();
        var request = RequestDispesaJsonBuilder.Build();
        var userCase = CreatUseCase(loggedUser);

        var result = await userCase.Execute(request);

        result.Should().NotBeNull();
        result.Titulo.Should().Be(request.Titulo);
    }

    [Fact]
    public async Task Error_Title_empty()
    {
        var loggedUser = UserBuild.Build();

        var request = RequestDispesaJsonBuilder.Build();
        request.Titulo = string.Empty;

        var userCase = CreatUseCase(loggedUser);

        var act = async () => await userCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.TITULO_OBRIGATORIO));
    }

    private RegisterDispesasUseCase CreatUseCase(Financas.Domain.Entidades.User user)
    {
        var repository = DespesasWriteOnlyRepositorioBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggeUser = LoggedUserBuilder.Build(user);
        
        return new RegisterDispesasUseCase(repository, unitOfWork,mapper,loggeUser);
    }

}
