using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;
using Xunit;

namespace SportCalendar.Tests;

public class EventServiceTests
{
    private readonly DbContextOptions<SportCalendarContext> _options;

    public EventServiceTests()
    {
        _options = new DbContextOptionsBuilder<SportCalendarContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    private async Task SeedData(SportCalendarContext context)
    {
        context.Sports.Add(new Sport { Id = 1, Name = "Football" });
        context.Places.Add(new Place { Id = 1, Name = "Kyiv Olympic", City = "Kyiv" });
        context.Places.Add(new Place { Id = 2, Name = "Polish Army", City = "Warsaw" });
        context.Teams.Add(new Team { Id = 1, Name = "Dynamo" });
        context.Teams.Add(new Team { Id = 2, Name = "Legia" });
        context.Teams.Add(new Team { Id = 3, Name = "Real Madrid" });
        context.Teams.Add(new Team { Id = 4, Name = "Barcelona" });
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateEventAsync_ShouldCreateEvent()
    {
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
        }

        var dto = new CreateEventDTO
        {
            Description = "Dynamo vs Legia Friendly Match",
            Start = DateTime.UtcNow,
            SportId = 1,
            PlaceId = 2,
            HomeTeamId = 1,
            AwayTeamId = 2
        };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            var result = await service.CreateEventAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(1, await context.Events.CountAsync());
        }
    }

    [Fact]
    public async Task GetEventsAsync_ShouldReturnAllEvents()
    {
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
            context.Events.Add(new Event { Description = "Dynamo vs Legia", Start = DateTimeOffset.UtcNow, SportId = 1, PlaceId = 2, HomeTeamId = 1, AwayTeamId = 2, Status = EventStatus.Scheduled });
            context.Events.Add(new Event { Description = "El Clasico: Real Madrid vs Barcelona", Start = DateTimeOffset.UtcNow, SportId = 1, PlaceId = 1, HomeTeamId = 3, AwayTeamId = 4, Status = EventStatus.Scheduled });
            await context.SaveChangesAsync();
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            var result = await service.GetEventsAsync();

            Assert.Equal(2, result.Count());
            Assert.All(result, e => Assert.NotNull(e.Sport));
        }
    }

    [Fact]
    public async Task GetEventAsync_ShouldReturnEventById()
    {
        int eventId;
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
            var ev = new Event { Description = "El Clasico", Start = DateTimeOffset.UtcNow, SportId = 1, PlaceId = 1, HomeTeamId = 3, AwayTeamId = 4, Status = EventStatus.Scheduled };
            context.Events.Add(ev);
            await context.SaveChangesAsync();
            eventId = ev.Id;
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            var result = await service.GetEventAsync(eventId);

            Assert.NotNull(result);
            Assert.Equal(eventId, result.Id);
            Assert.NotNull(result.Sport);
        }
    }

    [Fact]
    public async Task GetEventAsync_NonExistentId_ShouldReturnNull()
    {
        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            var result = await service.GetEventAsync(999);
            Assert.Null(result);
        }
    }

    [Fact]
    public async Task CreateEventAsync_SameTeam_ShouldThrowValidationException()
    {
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
        }

        var dto = new CreateEventDTO
        {
            Description = "Invalid Match",
            Start = DateTime.UtcNow,
            SportId = 1,
            PlaceId = 1,
            HomeTeamId = 1,
            AwayTeamId = 1 // Same team
        };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateEventAsync(dto));
        }
    }

    [Fact]
    public async Task CreateEventAsync_EmptyDescription_ShouldThrowValidationException()
    {
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
        }

        var dto = new CreateEventDTO
        {
            Description = "", // Invalid — Required
            Start = DateTime.UtcNow,
            SportId = 1,
            PlaceId = 1,
            HomeTeamId = 1,
            AwayTeamId = 2
        };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateEventAsync(dto));
        }
    }

    [Fact]
    public async Task CreateEventAsync_DescriptionTooLong_ShouldThrowValidationException()
    {
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
        }

        var dto = new CreateEventDTO
        {
            Description = new string('A', 201), // MaxLength is 200
            Start = DateTime.UtcNow,
            SportId = 1,
            PlaceId = 1,
            HomeTeamId = 1,
            AwayTeamId = 2
        };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateEventAsync(dto));
        }
    }

    [Fact]
    public async Task SetNegativeScore_ShouldFailValidation()
    {
        using (var context = new SportCalendarContext(_options))
        {
            await SeedData(context);
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new EventService(context);
            var dto = new CreateEventDTO
            {
                Description = "Dynamo vs Legia",
                Start = DateTime.UtcNow,
                SportId = 1,
                PlaceId = 2,
                HomeTeamId = 1,
                AwayTeamId = 2
            };
            var result = await service.CreateEventAsync(dto);
            result.HomeScore = -1; // Invalid negative score

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(result);
            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>(() =>
                System.ComponentModel.DataAnnotations.Validator.ValidateObject(result, validationContext, true));
        }
    }
}
