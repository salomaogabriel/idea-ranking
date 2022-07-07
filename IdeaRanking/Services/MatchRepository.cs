using IdeaRanking.Data;
using IdeaRanking.Models;
using Microsoft.EntityFrameworkCore;

namespace IdeaRanking.Services;

public class MatchRepository : IMatchRepository
{
    private readonly IdeaRankingContext _context;

    public MatchRepository(IdeaRankingContext context)
    {
        _context = context;
    }
    public async Task<Match> CreateMatch(Match match)
    {
        _context.Matches!.Add(match);
        await _context.SaveChangesAsync();
        return match;
    }

    public async Task<Match> GetMatch(int id)
    {
        return (await _context.Matches!
                .Include(i => i.IdeaOne)
                .Include(i => i.IdeaTwo)
                .SingleOrDefaultAsync(m => m.Id == id)
            )!;
    }

    public async Task<MatchesResponse> GetMatchesFromId(int id, int pageNo)
    {
        var response = new MatchesResponse();
        var matches = _context.Matches!.Where(c => c.IdeaOne!.Id == id || c.IdeaTwo!.Id == id);

            response.MaxPage = (int)Math.Ceiling(matches.Count() / 10.0);
            response.Matches = await matches.Include(i => i.IdeaOne)
            .Include(i => i.IdeaTwo)
            .OrderByDescending(m => m.Id)
            .Skip(pageNo * 10)
            .Take(10)
            .ToListAsync();
            return response;
    }

    public async Task<List<Match>> GetAllMatches(int pageNo)
    {
        return await _context.Matches!
            .Include(i => i.IdeaOne)
            .Include(i => i.IdeaTwo)
            .Skip(pageNo * 10)
            .OrderByDescending(m => m.Id)
            .Take(10)
            .ToListAsync();
    }

    public async Task<Match> EndMatch(int id, Winner winner)
    {
        var match = await GetMatch(id);
        match.Winner = winner;
        match.HasFinished = true;
        var updatedMatch = _context.Matches?.Update(match);
        await _context.SaveChangesAsync();
        return updatedMatch!.Entity;
    }

    public bool IsWinner(Match match, Idea idea)
    {
        switch (match.Winner)
        {
            case Winner.One:
                return idea == match.IdeaOne;
            case Winner.Two:
                return idea == match.IdeaTwo;
            default:
                return false;
        }
    }

    public async Task CreateHistory(Idea idea, bool HasWon)
    {
        var status = HasWon ? Status.Won : Status.Lost;
        var history = new IdeaHistory()
        {
            Idea = idea,
            Ranking = idea.Ranking,
            Status = status
        };
        _context.History!.Add(history);
        await _context.SaveChangesAsync();

    }
    public int GetMatchesMaxPages() => (int)Math.Ceiling(_context.Matches.Count() / 10.0);
}