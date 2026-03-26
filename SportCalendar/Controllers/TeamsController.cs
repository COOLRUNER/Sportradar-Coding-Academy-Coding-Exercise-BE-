using Microsoft.AspNetCore.Mvc;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;

namespace SportCalendar.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _teamService.GetTeamsAsync();
        return Ok(teams);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTeam(int id)
    {
        var team = await _teamService.GetTeamAsync(id);
        if (team is not null)
        {
            return Ok(team);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamDTO dto)
    {
        var createdTeam = await _teamService.CreateTeamAsync(dto);
        return CreatedAtAction(nameof(GetTeam), new { id = createdTeam.Id }, createdTeam);
    }
}
