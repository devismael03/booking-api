using BookingApi.Application.DTOs;
using BookingApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.WebApi.Controllers;

[ApiController]
[Route("api/available-homes")]
[Produces("application/json")]
public class AvailableHomesController : ControllerBase
{
    private readonly IHomeApplicationService _homeService;
    private readonly ILogger<AvailableHomesController> _logger;

    public AvailableHomesController(
        IHomeApplicationService homeService,
        ILogger<AvailableHomesController> logger)
    {
        _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(AvailableHomesResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AvailableHomesResponseDto>> Get(
        [FromQuery] string? startDate = null, 
        [FromQuery] string? endDate = null)
    {
        try
        {

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (!DateOnly.TryParse(startDate, out var parsedStartDate))
                {
                    return BadRequest("Invalid start date format. Please use YYYY-MM-DD format.");
                }

                if (!DateOnly.TryParse(endDate, out var parsedEndDate))
                {
                    return BadRequest("Invalid end date format. Please use YYYY-MM-DD format.");
                }

                if (parsedStartDate > parsedEndDate)
                {
                    return BadRequest("Start date cannot be after end date.");
                }

                _logger.LogInformation("Getting available homes for date range: {StartDate} to {EndDate}", 
                    parsedStartDate, parsedEndDate);
                
                var filteredResponse = await _homeService.GetAvailableHomesAsync(parsedStartDate, parsedEndDate);

                return Ok(filteredResponse);
            }
            else
            {
                return BadRequest("Both startDate and endDate parameters are required for date range filtering.");
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting available homes");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }
}