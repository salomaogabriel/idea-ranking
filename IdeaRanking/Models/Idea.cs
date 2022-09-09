using IdeaRanking.Models.Requests;
using Newtonsoft.Json;

namespace IdeaRanking.Models;

public class Idea
{
    public Idea()
    {
        Ranking = 800;
    }

    public Idea(IdeaRequest request)
    {
        Ranking = 800;
        BiggestRating = 800;
        Title = request.Title;
        Description = request.Description;
        Categories = new List<Category>();
    }

    public int Id { get; set; }
    public int Ranking { get; set; }
    public int NumberOfMatches { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public int BiggestRating { get; set; } = 0;

    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public List<Category>? Categories { get; set;}
    public List<IdeaHistory>? History { get; set; }

}