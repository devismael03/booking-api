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
            "123",
            "Home 1",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 15),
                new DateOnly(2025, 7, 16),
                new DateOnly(2025, 7, 17)
            });

        var home2 = new Home(
            "456",
            "Home 2",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 16),
                new DateOnly(2025, 7, 17),
                new DateOnly(2025, 7, 18)
            });

        var home3 = new Home(
            "789",
            "Home 3",
            new List<DateOnly>
            {
                new DateOnly(2025, 7, 17),
                new DateOnly(2025, 7, 18),
                new DateOnly(2025, 7, 19)
            });

        _homes.AddRange(new[] { home1, home2, home3 });
    }
}