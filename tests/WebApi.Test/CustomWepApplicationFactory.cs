using CommonTestUtilities.Entites;
using Financas.Domain.Security.Cryptography;
using Financas.Infrasctructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;
public class CustomWepApplicationFactory : WebApplicationFactory<Program>
{
    private Financas.Domain.Entidades.User _user;
    private string _password;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<FinancasDbContexto>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<FinancasDbContexto>();
                var passowordEncripter = scope.ServiceProvider.GetRequiredService<IPassowordEncripter>();

                StartDataBase(dbContext, passowordEncripter);
            });
    }

    public string GetEmail() => _user.Email;
    public string GetName() => _user.Nome;
    public string GetSenha() => _password;

    private void StartDataBase(FinancasDbContexto dbContexto, IPassowordEncripter passowordEncripter)
    {
        _user = UserBuild.Build();
        _password = _user.Senha;
        _user.Senha = passowordEncripter.Encrypt(_user.Senha);

        dbContexto.Users.Add(_user);

        dbContexto.SaveChanges();
    }
}
