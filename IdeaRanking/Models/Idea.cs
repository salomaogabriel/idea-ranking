using IdeaRanking.Models.Requests;

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
        // Categories = request.Categories;
    }
    public int Id { get; set; }
    public int Ranking { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public virtual List<Category>? Categories { get; set;}
    public virtual List<IdeaHistory>? History { get; set; }

}