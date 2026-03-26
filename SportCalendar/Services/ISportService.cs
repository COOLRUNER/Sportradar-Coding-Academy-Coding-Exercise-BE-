using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public interface ISportService
{
    Task<IEnumerable<Sport>> GetSportsAsync();
    Task<Sport?> GetSportAsync(int id);
    Task<Sport> CreateSportAsync(CreateSportDTO dto);
}
