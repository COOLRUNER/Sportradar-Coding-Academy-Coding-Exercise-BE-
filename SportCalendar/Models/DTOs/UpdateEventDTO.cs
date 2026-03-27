namespace SportCalendar.Models.DTOs;

public class UpdateEventDTO
{
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public EventStatus? Status { get; set; }
}
