using BookingApi.Application.Interfaces;
using BookingApi.Domain.Entities;

namespace BookingApi.Infrastructure.Repositories;

public class InMemoryHomeRepository : IHomeRepository
{
    private readonly List<Home> _homes;

    public InMemoryHomeRepository()
    {
        _homes = new List<Home>();
        SeedData();
    }

    public async Task<IEnumerable<Home>> GetAllAsync()
    {
        return await Task.FromResult(_homes.ToList());
    }

    private void SeedData()
    {
        var home1 = new Home(
            "H001",
            "House 1",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 15),
                new DateOnly(2025, 7, 16),
                new DateOnly(2025, 7, 17),
                new DateOnly(2025, 7, 18),
                new DateOnly(2025, 7, 19)
            });

        var home2 = new Home(
            "H002",
            "House 2",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 10),
                new DateOnly(2025, 7, 11),
                new DateOnly(2025, 7, 12),
                new DateOnly(2025, 7, 13),
                new DateOnly(2025, 7, 14),
                new DateOnly(2025, 7, 15),
                new DateOnly(2025, 7, 16)
            });

        var home3 = new Home(
            "H003",
            "House 3",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 12),
                new DateOnly(2025, 7, 15),
                new DateOnly(2025, 7, 18),
                new DateOnly(2025, 7, 22),
                new DateOnly(2025, 7, 25)
            });

        var home4 = new Home(
            "H004",
            "House 4",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 8),
                new DateOnly(2025, 7, 9),
                new DateOnly(2025, 7, 10),
                new DateOnly(2025, 7, 20),
                new DateOnly(2025, 7, 21),
                new DateOnly(2025, 7, 22),
                new DateOnly(2025, 7, 23),
                new DateOnly(2025, 7, 30),
                new DateOnly(2025, 7, 31)
            });

        var home5 = new Home(
            "H005",
            "House 5",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 14),
                new DateOnly(2025, 7, 15),
                new DateOnly(2025, 7, 16),
                new DateOnly(2025, 7, 17),
                new DateOnly(2025, 7, 24),
                new DateOnly(2025, 7, 25),
                new DateOnly(2025, 7, 26)
            });

        var home6 = new Home(
            "H006",
            "House 6",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 5),
                new DateOnly(2025, 7, 6),
                new DateOnly(2025, 7, 12),
                new DateOnly(2025, 7, 13),
                new DateOnly(2025, 7, 19),
                new DateOnly(2025, 7, 20),
                new DateOnly(2025, 7, 26),
                new DateOnly(2025, 7, 27)
            });

        var home7 = new Home(
            "H007",
            "House 7",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 16)
            });

        var home8 = new Home(
            "H008",
            "House 8",
            GenerateConsecutiveDates(new DateOnly(2025, 7, 1), new DateOnly(2025, 7, 31)));

        var home9 = new Home(
            "H009",
            "House 9",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 28),
                new DateOnly(2025, 7, 29),
                new DateOnly(2025, 7, 30),
                new DateOnly(2025, 7, 31),
                new DateOnly(2025, 8, 1),
                new DateOnly(2025, 8, 2),
                new DateOnly(2025, 8, 3)
            });

        var home10 = new Home(
            "H010",
            "House 10",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 7),
                new DateOnly(2025, 7, 8),
                new DateOnly(2025, 7, 11),
                new DateOnly(2025, 7, 12),
                new DateOnly(2025, 7, 13),
                new DateOnly(2025, 7, 16),
                new DateOnly(2025, 7, 19),
                new DateOnly(2025, 7, 20),
                new DateOnly(2025, 7, 21)
            });

        _homes.AddRange(new[] { home1, home2, home3, home4, home5, home6, home7, home8, home9, home10 });
    }

    private static List<DateOnly> GenerateConsecutiveDates(DateOnly start, DateOnly end)
    {
        var dates = new List<DateOnly>();
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            dates.Add(date);
        }
        return dates;
    }
}