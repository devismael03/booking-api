using BookingApi.Application.Interfaces;
using BookingApi.Domain.Entities;

namespace BookingApi.Infrastructure.Repositories;

public class InMemoryWeatherForecastRepository : IWeatherForecastRepository
{
    private readonly List<WeatherForecast> _forecasts;

    public InMemoryWeatherForecastRepository()
    {
        _forecasts = new List<WeatherForecast>();
        SeedData();
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await Task.FromResult(_forecasts.ToList());
    }

    private void SeedData()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", 
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        for (int i = 1; i <= 5; i++)
        {
            var date = today.AddDays(i);
            var temperatureC = Random.Shared.Next(-20, 55);
            var summary = summaries[Random.Shared.Next(summaries.Length)];
            
            _forecasts.Add(new WeatherForecast(date, temperatureC, summary));
        }
    }
}