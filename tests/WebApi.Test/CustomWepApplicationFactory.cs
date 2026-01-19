using Financas.Infrasctructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;
public class CustomWepApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Teste")
            .ConfigureServices(services =>
            {
                services.AddDbContext<FinancasDbContexto>(config =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    config.UseInMemoryDatabase("InMemoryForResting");
                    config.UseInternalServiceProvider(provider);
                });
            });
    }
}
