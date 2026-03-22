using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportCalendar.Models;

public class Event : IValidatableObject
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTimeOffset Start { get; set; }

    [Required]
    public EventStatus Status { get; set; }

    [Required, MaxLength(200)]
    public required string Description { get; set; }


    public int SportId { get; set; }
    public int? HomeTeamId { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Score cannot be negative.")]
    public int? HomeScore { get; set; }

    public int? AwayTeamId { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Score cannot be negative.")]
    public int? AwayScore { get; set; }

    public int PlaceId { get; set; }

    [ForeignKey(nameof(PlaceId))]
    public virtual Place? Place { get; set; }

    [ForeignKey(nameof(SportId))]
    public virtual Sport? Sport { get; set; }

    [ForeignKey(nameof(HomeTeamId))]
    [InverseProperty(nameof(Team.HomeEvents))]
    public virtual Team? HomeTeam { get; set; }

    [ForeignKey(nameof(AwayTeamId))]
    [InverseProperty(nameof(Team.AwayEvents))]
    public virtual Team? AwayTeam { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (HomeTeamId.HasValue && AwayTeamId.HasValue && HomeTeamId == AwayTeamId)
        {
            yield return new ValidationResult("A team cannot play against itself.", new[] { nameof(HomeTeamId), nameof(AwayTeamId) });
        }
    }
}