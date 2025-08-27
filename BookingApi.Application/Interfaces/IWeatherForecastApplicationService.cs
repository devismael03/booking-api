using BookingApi.Application.DTOs;

namespace BookingApi.Application.Interfaces;

public interface IWeatherForecastApplicationService
{
    Task<IEnumerable<WeatherForecastDto>> GetAllForecastsAsync();
}