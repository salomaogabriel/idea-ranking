using IdeaRanking.Models;
using Microsoft.EntityFrameworkCore;
namespace IdeaRanking.Data;

public static class SeedData
{
    // private static List<string> _categoryNames = new List<string>()
    // {
    //     "Software", "Website", "Finance", "Fintech", "Statistics", "Management", "School",
    //     "Food", "Quiz", "Gambling", "Games", "Math", "Movies", "Bussiness"
    // };
        public static void Initialize(IServiceProvider serviceProvider)
    {
        // using (var context = new IdeaRankingContext(
        //            serviceProvider.GetRequiredService<
        //                DbContextOptions<IdeaRankingContext>>()))
        // {
        //   var categories = _categoryNames.Select(s => new Category() {Name = s, Ideas = new List<Idea>()}).ToList();
        //     context.Categories.AddRange(categories);
        // }
    }
}