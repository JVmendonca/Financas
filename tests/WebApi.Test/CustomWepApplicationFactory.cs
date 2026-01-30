using CommonTestUtilities.Entites;
using Financas.Domain.Entidades;
using Financas.Domain.Security.Cryptography;
using Financas.Domain.Security.Tokens;
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
    private string _token;

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

                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccesTokenGeneretor>();
                _token = tokenGenerator.Generate(_user);
            });
    }

    public string GetEmail() => _user.Email;
    public string GetName() => _user.Nome;
    public string GetSenha() => _password;
    public string GetToken() => _token;

    private void StartDataBase(FinancasDbContexto dbContexto, IPassowordEncripter passowordEncripter)
    {
        AddUsers(dbContexto, passowordEncripter);
        AddDespesas(dbContexto, _user);
      
        dbContexto.SaveChanges();
    }
    private void AddUsers(FinancasDbContexto dbContexto, IPassowordEncripter passowordEncripter)
    {
        _user = UserBuild.Build();
        _password = _user.Senha;

        _user.Senha = passowordEncripter.Encrypt(_user.Senha);

        dbContexto.Users.Add(_user);

    }
    private void AddDespesas(FinancasDbContexto dbContexto, User user)
    {
        var despesa = DespesasBuilder.Build(user);

        dbContexto.Dispesas.Add(despesa);
    }
}
