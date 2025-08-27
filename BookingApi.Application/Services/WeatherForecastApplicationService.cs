using BookingApi.Application.DTOs;
using BookingApi.Application.Interfaces;

namespace BookingApi.Application.Services;

public class WeatherForecastApplicationService : IWeatherForecastApplicationService
{
    private readonly IWeatherForecastRepository _weatherRepository;

    public WeatherForecastApplicationService(IWeatherForecastRepository weatherRepository)
    {
        _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetAllForecastsAsync()
    {
        var forecasts = await _weatherRepository.GetAllAsync();
        return forecasts.Select(MapToDto);
    }

    private static WeatherForecastDto MapToDto(Domain.Entities.WeatherForecast forecast)
    {
        return new WeatherForecastDto
        {
            Date = forecast.Date,
            TemperatureC = forecast.TemperatureC,
            TemperatureF = forecast.TemperatureF,
            Summary = forecast.Summary
        };
    }
}