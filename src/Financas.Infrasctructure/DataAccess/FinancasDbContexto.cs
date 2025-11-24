using Microsoft.EntityFrameworkCore;
using Financas.Domain.Entidades;

namespace Financas.Infrasctructure.DataAccess;
internal class FinancasDbContexto : DbContext
{
    public FinancasDbContexto(DbContextOptions options) : base(options) { }
    public DbSet<Dispesa> Dispesas { get; set; }

}
 