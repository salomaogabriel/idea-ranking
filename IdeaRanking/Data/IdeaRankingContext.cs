using IdeaRanking.Models;
using Microsoft.EntityFrameworkCore;

// using IdeaRanking.Models;
namespace IdeaRanking.Data;

public class IdeaRankingContext : DbContext
{
    // public DbSet<Address> Address { get; set; }
    // public DbSet<Customer> Customers { get; set; }

    // Create a Table:
    // public DbSet<Model> TableName { get; set; }
    public IdeaRankingContext(DbContextOptions<IdeaRankingContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;

    }
    public DbSet<Idea>? Ideas { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<IdeaHistory>? History { get; set; }
    public DbSet<Match>? Matches { get; set; }
        
        
}