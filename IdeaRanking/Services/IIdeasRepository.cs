using IdeaRanking.Models;
using IdeaRanking.Models.Requests;

namespace IdeaRanking.Services;

public interface IIdeasRepository
{
    // TODO Pass Idea To Create
    Task<Idea> CreateIdea(IdeaRequest request);
    Task<List<Idea>>? GetIdeas(int pageNo);
    Task<Idea?> GetIdea(int id);
    Task<Idea?> UpdateIdeaRank(int rank, int id);
}