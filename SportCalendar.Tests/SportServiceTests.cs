using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;
using Xunit;

namespace SportCalendar.Tests;

public class SportServiceTests
{
    private readonly DbContextOptions<SportCalendarContext> _options;

    public SportServiceTests()
    {
        _options = new DbContextOptionsBuilder<SportCalendarContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task CreateSportAsync_ShouldCreateSport()
    {
        var dto = new CreateSportDTO { Name = "Tennis" };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new SportService(context);
            var result = await service.CreateSportAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(1, await context.Sports.CountAsync());
        }
    }

    [Fact]
    public async Task GetSportsAsync_ShouldReturnAllSports()
    {
        using (var context = new SportCalendarContext(_options))
        {
            context.Sports.Add(new Sport { Name = "Football" });
            context.Sports.Add(new Sport { Name = "Basketball" });
            await context.SaveChangesAsync();
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new SportService(context);
            var result = await service.GetSportsAsync();
            Assert.Equal(2, result.Count());
        }
    }

    [Fact]
    public async Task GetSportAsync_NonExistentId_ShouldReturnNull()
    {
        using (var context = new SportCalendarContext(_options))
        {
            var service = new SportService(context);
            var result = await service.GetSportAsync(999);
            Assert.Null(result);
        }
    }

    [Fact]
    public async Task CreateSportAsync_EmptyName_ShouldThrowValidationException()
    {
        var dto = new CreateSportDTO { Name = "" };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new SportService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateSportAsync(dto));
        }
    }

    [Fact]
    public async Task CreateSportAsync_NameTooLong_ShouldThrowValidationException()
    {
        var dto = new CreateSportDTO { Name = new string('A', 51) }; // MaxLength is 50

        using (var context = new SportCalendarContext(_options))
        {
            var service = new SportService(context);
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => service.CreateSportAsync(dto));
        }
    }
}
