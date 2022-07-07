using IdeaRanking.Data;
using IdeaRanking.Models;
using IdeaRanking.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IdeaRanking.Services;

public class IdeasRepository : IIdeasRepository
{
    private readonly IdeaRankingContext _context;

    public IdeasRepository(IdeaRankingContext context)
    {
        _context = context;
    }
    public async Task<Idea> CreateIdea(IdeaRequest request)
    {
        var idea = new Idea(request);
        _context.Ideas?.Add(idea);
        await _context.SaveChangesAsync();
        return idea;
    }

    public async Task<List<Idea>>? GetIdeas(int pageNo)
    {
        return await _context.Ideas?
            .Include(i => i.Categories)
            .Include(i => i.History!.Take(5))
            .OrderByDescending(i => i.Ranking)
            .Skip(pageNo * 10).Take(10).ToListAsync()!;
    }

    public async Task<Idea?> GetIdea(int id)
    {
        if (_context.Ideas != null)
            return await _context.Ideas
                .Include(i => i.Categories)
                .Include(i => i.History)
                .FirstOrDefaultAsync(i => i.Id == id);
        return null;
    }

    public async Task<Idea?> UpdateIdeaRank(int rank, int id)
    {
        
        var idea = await GetIdea(id);
        if (idea == null)
            return null;

        idea.Ranking = rank;
        
        var updatedIdea = _context.Ideas?.Update(idea);
        await _context.SaveChangesAsync();
        return updatedIdea?.Entity;
     
    }
}