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

        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Senha, config => config.Ignore())              // será criptografada no use case
            .ForMember(dest => dest.UserIndetificador, config => config.Ignore()); // será gerado Guid no use case
    }

    private void EntityToRequest()
    {
        CreateMap<Dispesa, ResponseDespesaJson>();
        CreateMap<Dispesa, ResponseShortExpenseJson>();
        CreateMap<Dispesa, ResponseDespesaIdJson>();

        
    }
}
