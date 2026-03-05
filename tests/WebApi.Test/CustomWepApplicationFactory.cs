using CommonTestUtilities.Entites;
using Financas.Domain.Entidades;
using Financas.Domain.Enums;
using Financas.Domain.Security.Cryptography;
using Financas.Domain.Security.Tokens;
using Financas.Infrasctructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resorces;

namespace WebApi.Test;
public class CustomWepApplicationFactory : WebApplicationFactory<Program>
{

    public DespesasIndentifyManeger Despesa_User_Admin { get; private set; } = default!;
    public DespesasIndentifyManeger Despesa_Member_Team { get; private set; } = default!;
    public UserIndentityManager User_Team_Member {  get; private set; } = default!;
    public UserIndentityManager User_Admin {  get; private set; } = default!;

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
                var accesTokenGeneretor = scope.ServiceProvider.GetRequiredService<IAccesTokenGeneretor>();


                StartDataBase(dbContext, passowordEncripter, accesTokenGeneretor);
            });
    }


    

    private void StartDataBase(FinancasDbContexto dbContexto,
        IPassowordEncripter passowordEncripter,
        IAccesTokenGeneretor accesTokenGeneretor)
    {
        var user_TeamMember = AddUserTeamMember(dbContexto, passowordEncripter, accesTokenGeneretor);
        var despesaTeamMember = AddDespesas(dbContexto, user_TeamMember, despesaId: 1);
        Despesa_Member_Team = new DespesasIndentifyManeger(despesaTeamMember);

        var user_Admin = AddUserAdmin(dbContexto, passowordEncripter, accesTokenGeneretor);
        var despesaUserAdmin = AddDespesas(dbContexto, user_Admin, despesaId: 2);
        Despesa_User_Admin = new DespesasIndentifyManeger(despesaUserAdmin);


        dbContexto.SaveChanges();
    }

    private User AddUserTeamMember(FinancasDbContexto dbContexto,
        IPassowordEncripter passowordEncripter, 
        IAccesTokenGeneretor accesTokenGeneretor)
    {
        var user = UserBuild.Build();
        user.Id = 1;

        var password = user.Senha;

        user.Senha = passowordEncripter.Encrypt(user.Senha);

        dbContexto.Users.Add(user);

        var token = accesTokenGeneretor.Generate(user);

        User_Team_Member = new UserIndentityManager(user, password, token );

        return user;

    }

    private User AddUserAdmin(FinancasDbContexto dbContexto,
        IPassowordEncripter passowordEncripter, 
        IAccesTokenGeneretor accesTokenGeneretor)
    {
        var user = UserBuild.Build(Regras.ADMIN);
        user.Id = 2;

        var password = user.Senha;

        user.Senha = passowordEncripter.Encrypt(user.Senha);

        dbContexto.Users.Add(user);

        var token = accesTokenGeneretor.Generate(user);

        User_Admin = new UserIndentityManager(user, password, token );

        return user;

    }
    private Dispesa AddDespesas(FinancasDbContexto dbContexto, User user, long despesaId)
    {
        var despesa = DespesasBuilder.Build(user);
        despesa.Id = despesaId;

        dbContexto.Dispesas.Add(despesa);

        return despesa;
    }
}
