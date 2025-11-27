using AutoMapper;
using Financas.Communication.Responses;
using Financas.Domain.Repositorios.Dispesas;

namespace Financas.Application.UseCases.Dispesas.GetById;
public class GetDespesasByIdUseCases : IGetDespesasByIdUseCases
{
    private readonly IDespesasRepositorio _despesaRepository;
    private readonly IMapper _mapper;

    public GetDespesasByIdUseCases(IDespesasRepositorio despesaRepository, IMapper mapper)
    {
        _despesaRepository = despesaRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDespesaIdJson> Execute(long id)
    {
        var result = await _despesaRepository.GetById(id);

        return _mapper.Map<ResponseDespesaIdJson>(result);
    }

}
