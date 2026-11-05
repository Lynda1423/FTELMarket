using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FTELMarket.Infrastructure;

public class FtelMarketDbContextFactory : IDesignTimeDbContextFactory<FtelMarketDbContext>
{
    public FtelMarketDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FtelMarketDbContext>();
        
        // Utilisation de la chaîne de connexion PostgreSQL
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ftelmarket_db;Username=postgres;Password=postgres");

        return new FtelMarketDbContext(optionsBuilder.Options);
    }
}