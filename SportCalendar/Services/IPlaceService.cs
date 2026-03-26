using SportCalendar.Models;
using SportCalendar.Models.DTOs;

namespace SportCalendar.Services;

public interface IPlaceService
{
    Task<IEnumerable<Place>> GetPlacesAsync();
    Task<Place?> GetPlaceAsync(int id);
    Task<Place> CreatePlaceAsync(CreatePlaceDTO dto);
}
