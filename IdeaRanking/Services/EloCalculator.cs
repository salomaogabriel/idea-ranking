using IdeaRanking.Models;

namespace IdeaRanking.Services;

public class EloCalculator : IEloCalculator
{
    public double GetProbability(int ratingOne, int ratingTwo)
    {
        return 1.0f * 1.0f / (1 + 1.0f *
            (double)(Math.Pow(10, 1.0f *
                (ratingOne - ratingTwo) / 400)));
    }

    public int GetNewRating(double probability, int kFactor, int actualRating, bool hasWon)
    {
        var variable = hasWon ? 1 : 0;
        return (int)(actualRating + kFactor * (variable - probability));
    }

    public int getKFactor(Idea idea)
    {
        if(idea.NumberOfMatches < 30)
            return 30;
        else if(idea.BiggestRating < 2400)
            return 15;
        else return 10;
    }
}