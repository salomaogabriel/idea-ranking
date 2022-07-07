namespace IdeaRanking.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public virtual List<Idea> Ideas { get; set; }
}