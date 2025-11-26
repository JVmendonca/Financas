using AutoMapper;
using Financas.Communication.Responses;
using Financas.Domain.Repositorios.Dispesas;

namespace Financas.Application.UseCases.Dispesas.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IDespesasRepositorio _repositorio;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IDespesasRepositorio repositorio, IMapper mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<ResponseDespesasjson> Execute()
    {
        var resultado = await _repositorio.GetAll();

        return new ResponseDespesasjson
        {
            Despesas = _mapper.Map<List<ResponseShortExpenseJson>>(resultado)
        };
    }
}
