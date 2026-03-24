using Microsoft.EntityFrameworkCore;
using Financas.Domain.Entidades;

namespace Financas.Infrasctructure.DataAccess;
public class FinancasDbContexto : DbContext
{
    public FinancasDbContexto(DbContextOptions options) : base(options) { }
    public DbSet<Dispesa> Dispesas { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>().ToTable("Tags");
    }
}
 