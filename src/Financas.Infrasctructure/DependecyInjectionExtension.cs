using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Security.Cryptography;
using Financas.Domain.Security.Tokens;
using Financas.Infrasctructure.DataAccess;
using Financas.Infrasctructure.DataAccess.Repositorios;
using Financas.Infrasctructure.DataAccess.Repositorios.User;
using Financas.Infrasctructure.Extensions;
using Financas.Infrasctructure.Security.Cryptography;
using Financas.Infrasctructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Financas.Infrasctructure;
public static class DependecyInjectionExtension
{
    public static void AddInfrasctructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPassowordEncripter, Crytptography>();

        AddToken(services, configuration);
        AddRepositorios(services);

        if (configuration.IsTesteEnvironment() == false)
        {
            AddDbContext(services, configuration);
        }
            
    }
    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMintes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccesTokenGeneretor>(config => new JwtTokenGenerator(expirationTimeMintes, signingKey!));
    }

    private static void AddRepositorios(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDespesasReadOnlyRepositorio, DispesasRepositorio>();
        services.AddScoped<IDespesasWriteOnlyRepositorio, DispesasRepositorio>();
        services.AddScoped<IDespesasUpdateOnlyRepositorio, DispesasRepositorio>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        
        var connectionString = configuration.GetConnectionString("conexaoBanco"); ;

        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<FinancasDbContexto>(config => config.UseMySql(connectionString, serverVersion));
    }
}
