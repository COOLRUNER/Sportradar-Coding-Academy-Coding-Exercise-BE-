using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;
using Xunit;

namespace SportCalendar.Tests;

public class TeamServiceTests
{
    private readonly DbContextOptions<SportCalendarContext> _options;

    public TeamServiceTests()
    {
        _options = new DbContextOptionsBuilder<SportCalendarContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task CreateTeamAsync_ShouldCreateTeam()
    {
        var dto = new CreateTeamDTO { Name = "Dynamo" };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new TeamService(context);
            var result = await service.CreateTeamAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(1, await context.Teams.CountAsync());
        }
    }

    [Fact]
    public async Task GetTeamsAsync_ShouldReturnAllTeams()
    {
        using (var context = new SportCalendarContext(_options))
        {
            context.Teams.Add(new Models.Team { Name = "Dynamo" });
            context.Teams.Add(new Models.Team { Name = "Legia" });
            await context.SaveChangesAsync();
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new TeamService(context);
            var result = await service.GetTeamsAsync();

            Assert.Equal(2, result.Count());
        }
    }
}
