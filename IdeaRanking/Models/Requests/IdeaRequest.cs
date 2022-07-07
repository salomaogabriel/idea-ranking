namespace IdeaRanking.Models.Requests;

public class IdeaRequest
{
    public int Id { get; set; }
    public int Ranking { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<string> NewCategories { get; set; }

}