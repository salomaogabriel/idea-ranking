using IdeaRanking.Models.Requests;
using IdeaRanking.Models;
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
        var maxPage = _ideasRepository.GetIdeasMaxPages();
        if (pageNo < 0) return BadRequest("Page needs to be bigger than 0!");
        if (pageNo > maxPage) return BadRequest($"Page needs to be smaller than {maxPage}!");

        var ideas = await _ideasRepository.GetIdeas(pageNo)!;
        if(ideas == null) return NotFound("There are no ideas");
        var response = new IdeasResponse();
        response.Ideas = ideas;
        response.MaxPage = maxPage;
        return Ok(response);
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
        if(idea == null) return NotFound($"An idea with id of {id} doesn't exst!");

        return Ok(idea);
    }
    // Create a new Idea
    [HttpPost]
    public async Task<IActionResult> CreateIdea(IdeaRequest request)
    {
        var idea = await _ideasRepository.CreateIdea(request);
        return CreatedAtAction(nameof(GetIdea), new { id = idea.Id }, idea);
    }
    // No time to create controller:
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {

        var categories = await _ideasRepository.GetCategories();
        if(categories == null) return NotFound($"There are no categories!");

        return Ok(categories);
    }
}