using BookingApi.Application.DTOs;
using BookingApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastApplicationService _weatherService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
        IWeatherForecastApplicationService weatherService,
        ILogger<WeatherForecastController> logger)
    {
        _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> Get()
    {
        try
        {
            _logger.LogInformation("Getting all weather forecast data");
            var forecasts = await _weatherService.GetAllForecastsAsync();
            return Ok(forecasts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting weather forecasts");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }
}