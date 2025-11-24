using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Dispesas;
using Financas.Infrasctructure.DataAccess;
using Financas.Infrasctructure.DataAccess.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Financas.Infrasctructure;
public static class DependecyInjectionExtension
{
    public static void AddInfrasctructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositorios(services);
    }

    private static void AddRepositorios(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDispesasRepositorio, DispesasRepositorio>();
    }
    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("conexaoBanco"); ;

        var version = new Version(8, 0, 44);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<FinancasDbContexto>(config => config.UseMySql(connectionString, serverVersion));
    }
}
