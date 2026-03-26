using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
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
            context.Sports.Add(new Models.Sport { Name = "Sport 1" });
            context.Sports.Add(new Models.Sport { Name = "Sport 2" });
            await context.SaveChangesAsync();
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new SportService(context);
            var result = await service.GetSportsAsync();
            Assert.Equal(2, result.Count());
        }
    }
}
