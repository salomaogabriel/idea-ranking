using IdeaRanking.Models.Requests;
using IdeaRanking.Services;
using Microsoft.AspNetCore.Mvc;


namespace IdeaRanking.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class IdeasController : ControllerBase
{

    private readonly IIdeasRepository _ideasRepository; 
    public IdeasController(IIdeasRepository ideasRepository)
    {
        _ideasRepository = ideasRepository;
    }

    // Get a List of Ranked Ideas by page Number, default = 0
    [HttpGet("page/{pageNo:int?}")]
    public async Task<IActionResult> GetRankedIdeas(int pageNo = 0)
    {
        if (pageNo < 0) return BadRequest("Page needs to be bigger than 0!");

        var ideas = await _ideasRepository.GetIdeas(pageNo)!;
        return Ok(ideas);
    }
    [HttpGet]
    public async Task<IActionResult> GetRankedIdeas()
    {

        return await GetRankedIdeas(0);
    }
    // Get Individual Idea
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIdea(int id)
    {

        var idea = await _ideasRepository.GetIdea(id);
        return Ok(idea);
    }
    // Create a new Idea
    [HttpPost]
    public async Task<IActionResult> CreateIdea(IdeaRequest request)
    {
        var idea = await _ideasRepository.CreateIdea(request);
        return CreatedAtAction(nameof(GetIdea), new { id = idea.Id }, idea);
    }
    
}