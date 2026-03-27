using Microsoft.AspNetCore.Mvc;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;

namespace SportCalendar.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var ev = await _eventService.GetEventAsync(id);
        if (ev is not null)
        {
            return Ok(ev);
        }
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventDTO dto)
    {
        var createdEvent = await _eventService.CreateEventAsync(dto);
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent(int id, UpdateEventDTO dto)
    {
        var updatedEvent = await _eventService.UpdateEventAsync(id, dto);
        if (updatedEvent is null)
        {
            return NotFound();
        }
        return Ok(updatedEvent);
    }
}