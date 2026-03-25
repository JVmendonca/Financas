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

        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Senha, config => config.Ignore())              // será criptografada no use case
            .ForMember(dest => dest.UserIndetificador, config => config.Ignore()); // será gerado Guid no use case

        CreateMap<RequestDispesaJson, Dispesa>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(src => src.Tags.Distinct()));

        CreateMap<Communication.Enuns.Tag, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(src => src)); // mapeamento direto do enum para a entidade Tag
    }


    private void EntityToRequest()
    {
        CreateMap<Dispesa, ResponseDespesaIdJson>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(src => src.Tags.Select(tag => tag.Value)));
        
        CreateMap<Dispesa, ResponseDespesaJson>();
        CreateMap<Dispesa, ResponseShortExpenseJson>();
        CreateMap<User, ResponseUserProfileJson>();

        
    }
}
