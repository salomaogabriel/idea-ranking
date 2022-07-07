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
        idea.Categories!.AddRange(await CreateCategories(request.NewCategories));
        idea.Categories!.AddRange(await GetCategories(request.CategoryIds));
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
        if(idea.Ranking > idea.BiggestRating)
        {
            idea.BiggestRating = rank;
        }
        
        var updatedIdea = _context.Ideas?.Update(idea);
        await _context.SaveChangesAsync();
        return updatedIdea?.Entity;
     
    }

    public async Task<Idea?> UpdateMatchesNo(int id, bool won)
    {
        var idea = await GetIdea(id);
        if (idea == null)
            return null;

        idea.NumberOfMatches += 1;
        if(won) idea.Wins += 1;
        var updatedIdea = _context.Ideas?.Update(idea);
        await _context.SaveChangesAsync();
        return updatedIdea?.Entity;
        
    }

    public async Task<Idea> GetRandomIdea()
    {
        return (await _context.Ideas
            .Include(i => i.Categories)
            .OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync())!;
    }

    public async Task<Idea> GetRandomIdea(int except)
    {
        return await _context.Ideas!
            .Include(i => i.Categories)
            .Where(i => i.Id != except)
            .OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync();

    }

    private async Task<List<Category>> CreateCategories(List<string> newCategories)
    {
        var categories = new List<Category>();
        foreach (var name in newCategories)
        {
            categories.Add(await CreateCategory(name));
        }

        await _context.SaveChangesAsync();
        return categories;
    }

    private async Task<Category> CreateCategory(string name)
    {
        var category = new Category()
        {
            Name = name
        };
        await _context.Categories!.AddAsync(category);
        return category;
    } 
    private async Task<List<Category>> GetCategories(List<int> categoryIds)
    {
        var categories = await _context.Categories!
            .IgnoreAutoIncludes().Where(c => categoryIds
                .Any(i => i == c.Id)).ToListAsync();
        return categories;
    }
    public int GetIdeasMaxPages() => (int)Math.Ceiling(_context.Ideas.Count() / 10.0);
    public async Task<List<Category>> GetCategories() => await _context.Categories.ToListAsync();

}