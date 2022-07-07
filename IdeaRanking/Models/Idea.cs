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
        Title = request.Title;
        Description = request.Description;
        Categories = new List<Category>();
    }

    public int Id { get; set; }
    public int Ranking { get; set; }
    public int NumberOfMatches { get; set; } = 0;

    public string? Title { get; set; }
    public string? Description { get; set; }
    
    [JsonIgnore]
    public virtual List<Category>? Categories { get; set;}
    public virtual List<IdeaHistory>? History { get; set; }

}