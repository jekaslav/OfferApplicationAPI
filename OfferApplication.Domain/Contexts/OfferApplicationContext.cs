using Microsoft.EntityFrameworkCore;
using OfferApplication.Domain.Entities;

namespace OfferApplication.Domain.Contexts;

public class OfferApplicationContext : DbContext
{
    public OfferApplicationContext(DbContextOptions<OfferApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<OfferEntity> Offers { get; set; } = null!;

    public DbSet<ProviderEntity> Providers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OfferEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Brand).IsRequired();
            entity.Property(e => e.Model).IsRequired();
        });

        modelBuilder.Entity<ProviderEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });
    }

}