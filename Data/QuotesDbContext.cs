using Microsoft.EntityFrameworkCore;
using WisdomWall.Models;

namespace WisdomWall.Data;

public class QuotesDbContext : DbContext
{
    public QuotesDbContext(DbContextOptions<QuotesDbContext> options) : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: Fluent API configuration can be placed here if needed
        modelBuilder.Entity<Quote>().ToTable("Quotes");
    }
}
