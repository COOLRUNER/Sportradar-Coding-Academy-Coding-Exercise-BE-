using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportCalendar.Models;

public class Team
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Event.HomeTeam))]
    public ICollection<Event> HomeEvents { get; set; } = new List<Event>();

    [InverseProperty(nameof(Event.AwayTeam))]
    public ICollection<Event> AwayEvents { get; set; } = new List<Event>();
}