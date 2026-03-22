using Microsoft.EntityFrameworkCore;
using SportCalendar.Models;

namespace SportCalendar.Data;

public class SportCalendarContext(DbContextOptions<SportCalendarContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Place> Places { get; set; } = null!;
    public DbSet<Sport> Sports { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .Property(e => e.Status)
            .HasConversion<string>();
    }
}
