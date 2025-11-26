using AutoMapper;
using Financas.Communication.Request;
using Financas.Communication.Responses;
using Financas.Domain.Entidades;

namespace Financas.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToRequest();
    }
    private void RequestToEntity()
    {
        CreateMap<RequestDispesaJson, Dispesa>();
    }
    private void EntityToRequest()
    {
        CreateMap<Dispesa, ResponseDespesaJson>();
        CreateMap<Dispesa, ResponseShortExpenseJson>();
    }
}
