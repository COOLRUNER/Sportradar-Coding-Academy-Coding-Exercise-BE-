using System.ComponentModel.DataAnnotations;

namespace SportCalendar.Models.DTOs;

public class CreateSportDTO
{
    [Required, MaxLength(50)]
    public required string Name { get; set; }
}
