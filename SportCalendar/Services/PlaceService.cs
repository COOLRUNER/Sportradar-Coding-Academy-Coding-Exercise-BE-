using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public class PlaceService : IPlaceService
{
    private readonly SportCalendarContext _context;

    public PlaceService(SportCalendarContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Place>> GetPlacesAsync()
    {
        return await _context.Places.AsNoTracking().ToListAsync();
    }

    public async Task<Place?> GetPlaceAsync(int id)
    {
        return await _context.Places.FindAsync(id);
    }

    public async Task<Place> CreatePlaceAsync(CreatePlaceDTO dto)
    {
        var place = new Place 
        { 
            Name = dto.Name,
            City = dto.City
        };
        _context.Places.Add(place);
        await _context.SaveChangesAsync();
        return place;
    }
}
