using AutoMapper;
using Financas.Communication.Responses;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Services.LoggedUser;


namespace Financas.Application.UseCases.Dispesas.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IDespesasReadOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllExpensesUseCase(IDespesasReadOnlyRepositorio repositorio, IMapper mapper, ILoggedUser loggedUser)
    {
        _repositorio = repositorio;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseDespesasjson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var resultado = await _repositorio.GetAll(loggedUser);

        return new ResponseDespesasjson
        {
            Despesas = _mapper.Map<List<ResponseShortExpenseJson>>(resultado)
        };
    }
}
