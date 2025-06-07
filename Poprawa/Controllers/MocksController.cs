using Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Poprawa.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private  readonly IDbService _dbService;

    public PatientsController(IDbService db)
    {
        _dbService = db;
    }
    
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetPatientInfo([FromRoute] int id)
    // {
    //     try
    //     {
    //         var patient = await _dbService.GetPatientData(id);
    //         return Ok(patient);
    //     }
    //     catch (NotFoundException e)
    //     {
    //         return NotFound(e.Message);
    //     }
    // }
    // [HttpPost]
    // public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDto prescription)
    // {
    //     try
    //     {
    //         await _dbService.AddPrescription(prescription);
    //         return Created();
    //     }
    //     catch (NotFoundException e)
    //     {
    //      return NotFound(e.Message);
    //     }
    //     catch (ConflictException e)
    //     {
    //      return Conflict(e.Message);
    //     }
    //     catch (BadRequestException e)
    //     {
    //      return BadRequest(e.Message);
    //     }
    // }
}