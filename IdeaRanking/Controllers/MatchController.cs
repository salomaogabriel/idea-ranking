using IdeaRanking.Models;
using IdeaRanking.Models.Requests;
using IdeaRanking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;


namespace IdeaRanking.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MatchController : ControllerBase
{

    private readonly IIdeasRepository _ideasRepository;
    private readonly IMatchRepository _matchRepository;
    private readonly IEloCalculator _eloCalculator;
    public MatchController(IIdeasRepository ideasRepository, IMatchRepository matchRepository
    , IEloCalculator eloCalculator)
    {
        _ideasRepository = ideasRepository;
        _matchRepository = matchRepository;
        _eloCalculator = eloCalculator;

    }

   
    // GET /vote -> Create a new Match
    [HttpGet("vote")]
    public async Task<IActionResult> CreateMatch()
    {
        // TODO: Come up with an way to give preference to the projects with less matches.
        var idea1 = await _ideasRepository.GetRandomIdea();
        var idea2 =await _ideasRepository.GetRandomIdea(idea1.Id);
        var match = new Match()
        {
            IdeaOne = idea1,
            IdeaTwo = idea2,
            PossibleOutcomeIdeaOne = _eloCalculator.GetProbability(idea2.Ranking, idea1.Ranking),
            PossibleOutcomeIdeaTwo = _eloCalculator.GetProbability(idea1.Ranking, idea2.Ranking)
        };
        await _matchRepository.CreateMatch(match);
        return Ok(match);
    }
    // GET /vote/{id} -> Get an existing Match
    [HttpGet("vote/{id}")]
    public async Task<IActionResult> GetExistingMatch(int id)
    {
        var match = await _matchRepository.GetMatch(id);
        return Ok(match);
    }
    // POST /vote -> Votes.
    [HttpPost]
    public async Task<IActionResult> Vote(VoteRequest request)
    {
        var match = await _matchRepository.GetMatch(request.Id);
        if (match.HasFinished) return BadRequest();
        
        var winner = request.IsFirstTheWinner ? Winner.One : Winner.Two;
            match.Winner = winner;
        await _ideasRepository.UpdateMatchesNo(match.IdeaOne!.Id);        
        await _ideasRepository.UpdateMatchesNo(match.IdeaTwo!.Id);
        await CalculateNewRanks(match);
        await _matchRepository.EndMatch(request.Id, match.Winner);
        return await CreateMatch();
    }

    private async Task CalculateNewRanks(Match match)
    {
        
        var kOne = _eloCalculator.getKFactor(match.IdeaOne!);
        var kTwo = _eloCalculator.getKFactor(match.IdeaTwo!);
        var newRankOne = _eloCalculator.GetNewRating(match.PossibleOutcomeIdeaOne,
            kOne,
            match.IdeaOne!.Ranking,
            _matchRepository.IsWinner(match, match.IdeaOne));
        
        var newRankTwo = _eloCalculator.GetNewRating(match.PossibleOutcomeIdeaTwo,
            kTwo,
            match.IdeaTwo!.Ranking,
            _matchRepository.IsWinner(match, match.IdeaTwo));
        
        var idea1 = await _ideasRepository.UpdateIdeaRank(newRankOne, match.IdeaOne.Id);
        var idea2 = await _ideasRepository.UpdateIdeaRank(newRankTwo, match.IdeaTwo.Id);

        await _matchRepository.CreateHistory(idea1!, _matchRepository.IsWinner(match, match.IdeaOne) );
        await _matchRepository.CreateHistory(idea2!, _matchRepository.IsWinner(match, match.IdeaTwo));

    }

    // GET / -> Get all matches with pageNo
    [HttpGet("{pageNo:int?}")]
    public async Task<IActionResult> GetAllMatches(int pageNo = 0)
    {
        if (pageNo < 0) return BadRequest("Page needs to be bigger than 0!");


        var matches = await _matchRepository.GetAllMatches(pageNo);
        return Ok(matches);
    }
    // GET /id/{id}/{pageNo} -> Get Matches from id with pageNo
    [HttpGet("id/{id:int}/{pageNo:int?}")]
    public async Task<IActionResult> GetMatchesFromIdea(int id, int pageNo = 0)
    {
        if (pageNo < 0) return BadRequest("Page needs to be bigger than 0!");

        var matches = await _matchRepository.GetMatchesFromId(id, pageNo);
        return Ok(matches);
    }


}