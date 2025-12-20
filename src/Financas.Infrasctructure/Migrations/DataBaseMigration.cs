using Financas.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Financas.Infrasctructure.Migrations;
public static class DataBaseMigration
{
    public async static Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<FinancasDbContexto>();

        await dbContext.Database.MigrateAsync();
    }
}
