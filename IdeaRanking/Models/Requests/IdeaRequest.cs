namespace IdeaRanking.Models.Requests;

public class IdeaRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<string> NewCategories { get; set; }

}