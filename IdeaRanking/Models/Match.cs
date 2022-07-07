namespace IdeaRanking.Models;

public enum Winner
{
    None,
    One,
    Two
};
public class Match
{
    public int Id { get; set; }
    public virtual Idea? IdeaOne { get; set; }
    public virtual Idea? IdeaTwo { get; set; }
    public double PossibleOutcomeIdeaOne { get; set; }
    public double PossibleOutcomeIdeaTwo { get; set; }
    public bool HasFinished { get; set; } = false;
    public Winner Winner { get; set; } = Winner.None;

}