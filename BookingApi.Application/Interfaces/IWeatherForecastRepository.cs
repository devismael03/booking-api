using BookingApi.Domain.Entities;

namespace BookingApi.Application.Interfaces;

public interface IWeatherForecastRepository
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
}