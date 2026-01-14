using AutoMapper;
using Financas.Application.AutoMapper;

namespace CommonTestUtilities.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapperConfig = new MapperConfiguration(config =>
        { 
            config.AddProfile(new AutoMapping());
        });
        return mapperConfig.CreateMapper();
    }
}
