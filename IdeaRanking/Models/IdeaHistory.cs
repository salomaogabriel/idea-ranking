namespace IdeaRanking.Models;

public class IdeaHistory
{
    public int Id { get; set; }
    public virtual Idea Idea { get; set; }
    public int Ranking { get; set; }
}