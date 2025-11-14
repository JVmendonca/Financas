using Microsoft.EntityFrameworkCore;
using Financas.Domain.Entidades;

namespace Financas.Infrasctructure.DataAccess;
public class FinancasDbContexto : DbContext
{
    public DbSet<Dispesa> Dispesas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;database=financasdb;Uid=root;pwd=24083066Jj*;";

        var version = new Version(8, 0, 44));
        var serverVersion = new MySqlServerVersion(version);

        optionsBuilder.UseMySql(connectionString,serverVersion );
    }
}
 