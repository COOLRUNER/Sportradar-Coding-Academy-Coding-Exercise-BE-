using Microsoft.AspNetCore.Mvc;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;

namespace SportCalendar.Controllers;

[ApiController]
[Route("api/sports")]
public class SportsController : ControllerBase
{
    private readonly ISportService _sportService;

    public SportsController(ISportService sportService)
    {
        _sportService = sportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSports()
    {
        var sports = await _sportService.GetSportsAsync();
        return Ok(sports);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSport(int id)
    {
        var sport = await _sportService.GetSportAsync(id);
        if (sport is not null)
        {
            return Ok(sport);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSport(CreateSportDTO dto)
    {
        var createdSport = await _sportService.CreateSportAsync(dto);
        return CreatedAtAction(nameof(GetSport), new { id = createdSport.Id }, createdSport);
    }
}
