using Financas.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Financas.Infrasctructure.Migrations;
public static class DataBaseMigration
{
    public static async Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<FinancasDbContexto>();

        await dbContext.Database.MigrateAsync();
    }
}
