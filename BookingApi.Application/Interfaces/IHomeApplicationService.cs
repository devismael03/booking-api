using BookingApi.Application.DTOs;

namespace BookingApi.Application.Interfaces;

public interface IHomeApplicationService
{
    Task<AvailableHomesResponseDto> GetAvailableHomesAsync(DateOnly startDate, DateOnly endDate);
}