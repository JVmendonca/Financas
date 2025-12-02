using AutoMapper;
using Financas.Communication.Responses;
using Financas.Domain.Repositorios.Despesas;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.GetById;
public class GetDespesasByIdUseCases : IGetDespesasByIdUseCases
{
    private readonly IDespesasReadOnlyRepositorio _despesaRepository;
    private readonly IMapper _mapper;

    public GetDespesasByIdUseCases(IDespesasReadOnlyRepositorio despesaRepository, IMapper mapper)
    {
        _despesaRepository = despesaRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDespesaIdJson> Execute(long id)
    {
        var result = await _despesaRepository.GetById(id);

        if (result == null)
        {
            throw new NotFoundExeption(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA);
        }

        return _mapper.Map<ResponseDespesaIdJson>(result);
    }

}
