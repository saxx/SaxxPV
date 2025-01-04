using Microsoft.EntityFrameworkCore;

namespace SaxxPv.Web.Models.Database;

public class Db(DbContextOptions options) : DbContext(options)
{
    public DbSet<Pricing> Pricings => Set<Pricing>();
    public DbSet<Reading> Readings => Set<Reading>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Reading>(b => { b.HasKey(x => x.DateTime); });
        builder.Entity<Pricing>(b =>
        {
            b.HasKey(x => new
            {
                x.From,
                x.To
            });
        });
    }
}
