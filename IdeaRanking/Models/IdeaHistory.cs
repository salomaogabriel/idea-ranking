namespace IdeaRanking.Models;

public enum Status
{
    Won,
    Undefined,
    Lost
};
public class IdeaHistory
{
    public int Id { get; set; }
    public virtual Idea Idea { get; set; }
    public int Ranking { get; set; }
    public Status Status { get; set; }
}