using Microsoft.EntityFrameworkCore;

using SportCalendar.Data;
using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public class EventService : IEventService
{
    private readonly SportCalendarContext _context;
    public EventService(SportCalendarContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        return await _context.Events
            .Include(e => e.Sport)
            .Include(e => e.Place)
            .Include(e => e.HomeTeam)
            .Include(e => e.AwayTeam)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Event?> GetEventAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Sport)
            .Include(e => e.Place)
            .Include(e => e.HomeTeam)
            .Include(e => e.AwayTeam)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event> CreateEventAsync(CreateEventDTO dto)
    {
        var newEvent = new Event
        {
            Description = dto.Description,
            Start = dto.Start.ToUniversalTime(),
            Status = dto.Status ?? EventStatus.Scheduled,
            SportId = dto.SportId,
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            PlaceId = dto.PlaceId
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return newEvent;
    }
}
