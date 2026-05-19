using Microsoft.EntityFrameworkCore;
using Quotes.com.Models;

namespace Quotes.com.Data;

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
