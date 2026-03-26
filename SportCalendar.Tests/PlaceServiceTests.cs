using Microsoft.EntityFrameworkCore;
using SportCalendar.Data;
using SportCalendar.Models.DTOs;
using SportCalendar.Services;
using Xunit;

namespace SportCalendar.Tests;

public class PlaceServiceTests
{
    private readonly DbContextOptions<SportCalendarContext> _options;

    public PlaceServiceTests()
    {
        _options = new DbContextOptionsBuilder<SportCalendarContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task CreatePlaceAsync_ShouldCreatePlace()
    {
        var dto = new CreatePlaceDTO { Name = "Kyiv Olympic Stadium", City = "Kyiv" };

        using (var context = new SportCalendarContext(_options))
        {
            var service = new PlaceService(context);
            var result = await service.CreatePlaceAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.City, result.City);
            Assert.Equal(1, await context.Places.CountAsync());
        }
    }

    [Fact]
    public async Task GetPlacesAsync_ShouldReturnAllPlaces()
    {
        using (var context = new SportCalendarContext(_options))
        {
            context.Places.Add(new Models.Place { Name = "Kyiv Olympic Stadium", City = "Kyiv" });
            context.Places.Add(new Models.Place { Name = "Polish Army Stadium", City = "Warsaw" });
            await context.SaveChangesAsync();
        }

        using (var context = new SportCalendarContext(_options))
        {
            var service = new PlaceService(context);
            var result = await service.GetPlacesAsync();

            Assert.Equal(2, result.Count());
        }
    }
}

