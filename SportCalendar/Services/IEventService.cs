using SportCalendar.Models;

using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public interface IEventService
{
    Task<IEnumerable<Event>> GetEventsAsync();
    Task<Event?> GetEventAsync(int id);
    Task<Event> CreateEventAsync(CreateEventDTO dto);
    Task<Event?> UpdateEventAsync(int id, UpdateEventDTO dto);
}
