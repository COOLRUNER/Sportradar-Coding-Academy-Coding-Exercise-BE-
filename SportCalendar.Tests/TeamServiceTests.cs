using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models;
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
            context.Teams.Add(new Team { Name = "Dynamo" });
            context.Teams.Add(new Team { Name = "Legia" });
            await context.SaveChangesAsync();
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new TeamService(context);
            var result = await service.GetTeamsAsync();
            Assert.Equal(2, result.Count());
        }
    }

    [Fact]
    public async Task GetTeamAsync_NonExistentId_ShouldReturnNull()
    {
        using (var context = new SportCalendarContext(_options))
        {
            var service = new TeamService(context);
            var result = await service.GetTeamAsync(999);
            Assert.Null(result);
        }
    }

    [Fact]
    public async Task CreateTeamAsync_EmptyName_ShouldThrowValidationException()
    {
        var dto = new CreateTeamDTO { Name = "" };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new TeamService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateTeamAsync(dto));
        }
    }

    [Fact]
    public async Task CreateTeamAsync_NameTooLong_ShouldThrowValidationException()
    {
        var dto = new CreateTeamDTO { Name = new string('A', 51) }; // MaxLength is 50

        using (var context = new SportCalendarContext(_options))
        {
            var service = new TeamService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateTeamAsync(dto));
        }
    }
}
