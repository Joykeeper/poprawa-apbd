using Kol2Preparation.Exceptions;
using Poprawa.Services;
using Microsoft.AspNetCore.Mvc;
using Poprawa.DTOs;

namespace Poprawa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtifactsController : ControllerBase
{
    private  readonly IDbService _dbService;

    public ArtifactsController(IDbService db)
    {
        _dbService = db;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddArtifact([FromBody] NewArtifactProjectDto artifactProject)
    {
        try
        {
            await _dbService.AddArtifact(artifactProject);
            return Created();
        }
        catch (NotFoundException e)
        {
         return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
         return Conflict(e.Message);
        }
        catch (Exception e)
        {
         return BadRequest(e.Message);
        }
    }
}