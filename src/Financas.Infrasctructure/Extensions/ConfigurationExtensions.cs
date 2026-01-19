using Microsoft.Extensions.Configuration;

namespace Financas.Infrasctructure.Extensions;
public static class ConfigurationExtensions
{
    public static bool IsTesteEnvironment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}
