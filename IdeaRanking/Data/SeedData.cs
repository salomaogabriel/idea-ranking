using IdeaRanking.Models;
using Microsoft.EntityFrameworkCore;
namespace IdeaRanking.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new IdeaRankingContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<IdeaRankingContext>>()))
        {
            var category1 = new Category()
            {
                Name = "Games"
            };
            var category2 = new Category()
            {
                Name = "Software"
            };
            var category3 = new Category()
            {
                Name = "All"
            };
            // Look for any movies.
            if (context.Categories.Any()) { return; }
            context.Categories.AddRange(
                category1,
                category2,
                category3
                );

           
            context.SaveChanges();
        }
    }
}