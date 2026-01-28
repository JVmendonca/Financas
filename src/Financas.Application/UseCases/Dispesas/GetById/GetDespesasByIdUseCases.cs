using AutoMapper;
using Financas.Communication.Responses;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Services.LoggedUser;
using Financas.Exeption;
using Financas.Exeption.ExeptionBase;

namespace Financas.Application.UseCases.Dispesas.GetById;
public class GetDespesasByIdUseCases : IGetDespesasByIdUseCases
{
    private readonly IDespesasReadOnlyRepositorio _despesaRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetDespesasByIdUseCases(IDespesasReadOnlyRepositorio despesaRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _despesaRepository = despesaRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseDespesaIdJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _despesaRepository.GetById(loggedUser, id);

        if (result == null)
        {
            throw new NotFoundExeption(ResourceErrorMassages.DESPESA_NAO_ENCONTRADA);
        }

        return _mapper.Map<ResponseDespesaIdJson>(result);
    }

}
