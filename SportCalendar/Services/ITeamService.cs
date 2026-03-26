using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public interface ITeamService
{
    Task<IEnumerable<Team>> GetTeamsAsync();
    Task<Team?> GetTeamAsync(int id);
    Task<Team> CreateTeamAsync(CreateTeamDTO dto);
}
