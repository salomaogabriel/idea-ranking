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
    Task<Idea?> UpdateMatchesNo(int id, bool won);
    Task<Idea> GetRandomIdea();
    Task<Idea> GetRandomIdea(int except);
    int GetIdeasMaxPages();
    Task<List<Category>> GetCategories();
}