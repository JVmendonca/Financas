using CommonTestUtilities.Entites;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorios;
using Financas.Application.UseCases.Dispesas.GetById;
using Financas.Domain.Entidades;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;
using FluentAssertions;

namespace UseCase.Test.Despesas.GetById;
public class GetDespesasByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuild.Build();
        var despesa = DespesasBuilder.Build(loggedUser);

        var useCase = CreatUseCase(loggedUser, despesa);

        var result = await useCase.Execute(despesa.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(despesa.Id);
        result.Titulo.Should().Be(despesa.Titulo);
        result.Descricao.Should().Be(despesa.Descricao);
        result.Data.Should().Be(despesa.Data);
        result.Valor.Should().Be(despesa.Valor);
        result.Pagamento.Should().Be((Financas.Communication.Enuns.PaymentType)despesa.Pagamento);
        result.Tags.Should().NotBeNullOrEmpty().And.BeEquivalentTo(despesa.Tags.Select(t => t.Value));

    }

    [Fact]
    public async Task Error_Despesa_Not_Found()
    {
        var loggedUser = UserBuild.Build();
        var useCase = CreatUseCase(loggedUser);
        
        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundExeption>();

        result.Where(ex => ex.GetErros().Count == 1 && ex.GetErros().Contains(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA));
    }

    private GetDespesasByIdUseCases CreatUseCase(User user, Dispesa? despesa = null)
    {
       var repository = new DespesasReadOnlyRepositorioBuilder().GetById(user, despesa).Build();
       var mapper = MapperBuilder.Build();
       var loggedUser = LoggedUserBuilder.Build(user);
       
       return new GetDespesasByIdUseCases(repository, mapper, loggedUser);
    }
}
