using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorios;
using CommonTestUtilities.Requests;
using Financas.Application.UseCases.Despesas.Update;
using Financas.Domain.Entidades;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace UseCase.Test.Despesas.Update;
public class UpdateDespesaUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuild.Build();
        var request = RequestDespesaJsonBuilder.Build();
        var despesa = DespesasBuilder.Build(loggedUser);

        var useCase = CreatUseCase(loggedUser, despesa);

        var act = async () => await useCase.Execute(despesa.Id, request);

        await act.Should().NotThrowAsync();

        despesa.Titulo.Should().Be(request.Titulo);
        despesa.Descricao.Should().Be(request.Descricao);
        despesa.Data.Should().Be(request.Data);
        despesa.Valor.Should().Be(request.Valor);
        despesa.Pagamento.Should().Be((Financas.Domain.Enuns.PaymentType)request.Pagamento);

    }
    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuild.Build();
        var despesa = DespesasBuilder.Build(loggedUser);

        var request = RequestDespesaJsonBuilder.Build();
        request.Titulo = string.Empty;

        var useCase = CreatUseCase(loggedUser, despesa);

        var act = async () => await useCase.Execute(despesa.Id, request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.TITULO_OBRIGATORIO));
    }
    [Fact]
    public async Task Error_Despesa_Not_Foud()
    {
        var loggedUser = UserBuild.Build();

        var request = RequestDespesaJsonBuilder.Build();

        var useCase = CreatUseCase(loggedUser); 

        var act = async () => await useCase.Execute(id: 1000, request);

        var result = await act.Should().ThrowAsync<NotFoundExeption>();

        await act.Should()
        .ThrowAsync<NotFoundExeption>() 
        .Where(ex => ex.GetErros().Count == 1 &&
                     ex.GetErros().Contains(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA));

    }

    private UpdateDespesaUseCase CreatUseCase(User user, Despesa? despesa = null)
    {
        var repository = new DespesasUpdateOnlyRepositoryBuilder().GetById(user, despesa).Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggdUser = LoggedUserBuilder.Build(user);


        return new UpdateDespesaUseCase(mapper, unitOfWork, repository, loggdUser);
    
    }
}
