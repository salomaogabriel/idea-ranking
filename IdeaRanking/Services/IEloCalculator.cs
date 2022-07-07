using IdeaRanking.Models;

namespace IdeaRanking.Services;

public interface IEloCalculator
{
    double GetProbability(int ratingOne, int ratingTwo);

    int GetNewRating(double probability, int k, int actualRating, bool hasWon);
    int getKFactor(Idea idea);
}