using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportCalendar.Models;

public class Result
{
    [Key]
    public int Id { get; set; }

    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }

    [MaxLength(100)]
    public string? Winner { get; set; }
    public int? WinnerId { get; set; }
    
    [MaxLength(200)]
    public string? Message { get; set; }

    public int EventId { get; set; }
    
    [ForeignKey(nameof(EventId))]
    public virtual Event? Event { get; set; }

    public List<string> Goals { get; set; } = new();
    public List<string> YellowCards { get; set; } = new();
    public List<string> SecondYellowCards { get; set; } = new();
    public List<string> DirectRedCards { get; set; } = new();
}
