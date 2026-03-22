using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportCalendar.Models;

public class Place
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public required string Name { get; set; }
    [Required, MaxLength(50)]
    public required string City { get; set; }
    public ICollection<Event> Events { get; set; } = new List<Event>();
}