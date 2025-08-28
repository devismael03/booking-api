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

    public async Task<AvailableHomesResponseDto> GetAvailableHomesAsync()
    {
        var homes = await _homeRepository.GetAllAsync();
        
        var homeDtos = homes.Select(MapToDto).ToList();
        
        return new AvailableHomesResponseDto
        {
            Status = "OK",
            Homes = homeDtos
        };
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