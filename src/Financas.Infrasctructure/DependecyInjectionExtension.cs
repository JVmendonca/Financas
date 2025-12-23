using Financas.Domain.Repositorios;
using Financas.Domain.Repositorios.Despesas;
using Financas.Domain.Repositorios.User;
using Financas.Domain.Security.Cryptography;
using Financas.Infrasctructure.DataAccess;
using Financas.Infrasctructure.DataAccess.Repositorios;
using Financas.Infrasctructure.DataAccess.Repositorios.User;
using Financas.Infrasctructure.Security;
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

        services.AddScoped<IPassowordEncripter, Crytptography>();
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

        var version = new Version(8, 0, 44);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<FinancasDbContexto>(config => config.UseMySql(connectionString, serverVersion));
    }
}
