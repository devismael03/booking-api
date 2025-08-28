using BookingApi.Application.DTOs;
using BookingApi.Application.Interfaces;

namespace BookingApi.Application.Services;

public class HomeApplicationService : IHomeApplicationService
{
    private readonly IHomeRepository _homeRepository;

    public HomeApplicationService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository ?? throw new ArgumentNullException(nameof(homeRepository));
    }

    public async Task<AvailableHomesResponseDto> GetAvailableHomesAsync(DateOnly startDate, DateOnly endDate)
    {
        var allHomes = await _homeRepository.GetAllAsync();
        
        var filteredHomes = allHomes.Where(home => IsHomeAvailableForDateRange(home, startDate, endDate));
        
        var homeDtos = filteredHomes.Select(MapToDto).ToList();
        
        return new AvailableHomesResponseDto
        {
            Status = "OK",
            Homes = homeDtos
        };
    }

    private static bool IsHomeAvailableForDateRange(Domain.Entities.Home home, DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
            return false;

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (!home.IsAvailableOn(date))
            {
                return false;
            }
        }
        
        return true;
    }

    private static HomeDto MapToDto(Domain.Entities.Home home)
    {
        return new HomeDto
        {
            HomeId = home.HomeId,
            HomeName = home.HomeName,
            AvailableSlots = home.AvailableSlots.Select(date => date.ToString("yyyy-MM-dd")).ToList()
        };
    }
}