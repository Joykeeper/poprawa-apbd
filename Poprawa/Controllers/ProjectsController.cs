using Kol2Preparation.Exceptions;
using Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Poprawa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private  readonly IDbService _dbService;

    public ProjectsController(IDbService db)
    {
        _dbService = db;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectInfo([FromRoute] int id)
    {
        try
        {
            var project = await _dbService.GetProjectInfo(id);
            return Ok(project);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}