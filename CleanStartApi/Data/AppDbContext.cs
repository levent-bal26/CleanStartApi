using Microsoft.EntityFrameworkCore;
using CleanStartApi.Models;

namespace CleanStartApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Urun> Urunler => Set<Urun>();
    public DbSet<Marka> Markalar => Set<Marka>();
    public DbSet<Sube> Subeler => Set<Sube>();
    public DbSet<Satis> Satislar => Set<Satis>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Urun>().HasIndex(x => x.Ad).IsUnique();
        modelBuilder.Entity<Marka>().HasIndex(x => x.Ad).IsUnique();
        modelBuilder.Entity<Sube>().HasIndex(x => x.Ad).IsUnique();

        modelBuilder.Entity<Satis>()
            .Property(x => x.BirimFiyat)
            .HasPrecision(18, 2);
    }
}

