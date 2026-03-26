namespace SportCalendar.Models.DTOs;

public class CreateEventDTO
{
    public required string Description { get; set; }
    public DateTime Start { get; set; }
    public EventStatus? Status { get; set; }
    public int SportId { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int PlaceId { get; set; }
}

