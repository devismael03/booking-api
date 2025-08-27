namespace BookingApi.Domain.Entities;

public class WeatherForecast
{
    public WeatherForecast(DateOnly date, int temperatureC, string summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
    }

    public DateOnly Date { get; private set; }

    public int TemperatureC { get; private set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; private set; }

    public void UpdateTemperature(int temperatureC)
    {
        TemperatureC = temperatureC;
    }

    public void UpdateSummary(string summary)
    {
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
    }
}