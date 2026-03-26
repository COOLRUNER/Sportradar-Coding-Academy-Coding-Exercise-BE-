using System.ComponentModel.DataAnnotations;

namespace SportCalendar.Models.DTOs;

public class CreatePlaceDTO
{
    [Required, MaxLength(50)]
    public required string Name { get; set; }
    [Required, MaxLength(50)]
    public required string City { get; set; }
}
