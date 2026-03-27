namespace SportCalendar.Web.Models;

public record EventDto(
    int Id,
    string Description,
    DateTimeOffset Start,
    string Status,
    int SportId,
    SportDto? Sport,
    int? HomeTeamId,
    TeamDto? HomeTeam,
    int? HomeScore,
    int? AwayTeamId,
    TeamDto? AwayTeam,
    int? AwayScore,
    int PlaceId,
    PlaceDto? Place
);

public record SportDto(int Id, string Name);
public record TeamDto(int Id, string Name);
public record PlaceDto(int Id, string Name, string City);
