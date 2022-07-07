namespace IdeaRanking.Models.Requests;

public class VoteRequest
{
    public int Id { get; set; }
    public bool IsFirstTheWinner { get; set; }
}