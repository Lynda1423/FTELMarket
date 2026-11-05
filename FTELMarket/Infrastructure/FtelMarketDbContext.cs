using FTELMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTELMarket.Infrastructure;

public class FtelMarketDbContext : DbContext
{
    public FtelMarketDbContext(DbContextOptions<FtelMarketDbContext> options) : base(options)
    {
    }

    // Déclaration des tables en base de données
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Charge automatiquement toutes les configurations du dossier Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FtelMarketDbContext).Assembly);
    }
}