using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public class TeamService : ITeamService
{
    private readonly SportCalendarContext _context;

    public TeamService(SportCalendarContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        return await _context.Teams.AsNoTracking().ToListAsync();
    }

    public async Task<Team?> GetTeamAsync(int id)
    {
        return await _context.Teams.FindAsync(id);
    }

    public async Task<Team> CreateTeamAsync(CreateTeamDTO dto)
    {
        var team = new Team { Name = dto.Name };

        var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(team);
        System.ComponentModel.DataAnnotations.Validator.ValidateObject(team, validationContext, validateAllProperties: true);

        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }
}

