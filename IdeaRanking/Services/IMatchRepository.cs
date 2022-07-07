using IdeaRanking.Models;

namespace IdeaRanking.Services;

public interface IMatchRepository
{
    Task<Match> CreateMatch(Match match);
    Task<Match> GetMatch(int id);
    Task<MatchesResponse> GetMatchesFromId(int id, int pageNo);
    Task<List<Match>> GetAllMatches(int pageNo);
    Task<Match> EndMatch(int id, Winner winner);
    bool IsWinner(Match match, Idea idea);
    Task CreateHistory(Idea idea, bool hasWon);
    int GetMatchesMaxPages();
}