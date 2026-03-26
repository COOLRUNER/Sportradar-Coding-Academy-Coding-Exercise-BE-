using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public class SportService : ISportService
{
    private readonly SportCalendarContext _context;

    public SportService(SportCalendarContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sport>> GetSportsAsync()
    {
        return await _context.Sports.AsNoTracking().ToListAsync();
    }

    public async Task<Sport?> GetSportAsync(int id)
    {
        return await _context.Sports.FindAsync(id);
    }

    public async Task<Sport> CreateSportAsync(CreateSportDTO dto)
    {
        var sport = new Sport { Name = dto.Name };
        _context.Sports.Add(sport);
        await _context.SaveChangesAsync();
        return sport;
    }
}
