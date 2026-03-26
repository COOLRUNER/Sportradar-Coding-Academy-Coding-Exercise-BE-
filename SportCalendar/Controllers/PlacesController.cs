using Microsoft.AspNetCore.Mvc;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;

namespace SportCalendar.Controllers;

[ApiController]
[Route("api/places")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _placeService;

    public PlacesController(IPlaceService placeService)
    {
        _placeService = placeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlaces()
    {
        var places = await _placeService.GetPlacesAsync();
        return Ok(places);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPlace(int id)
    {
        var place = await _placeService.GetPlaceAsync(id);
        if (place is not null)
        {
            return Ok(place);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlace(CreatePlaceDTO dto)
    {
        var createdPlace = await _placeService.CreatePlaceAsync(dto);
        return CreatedAtAction(nameof(GetPlace), new { id = createdPlace.Id }, createdPlace);
    }
}
