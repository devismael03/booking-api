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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AvailableHomesResponseDto>> Get()
    {
        try
        {
            _logger.LogInformation("Getting all available homes");
            var response = await _homeService.GetAvailableHomesAsync();
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting available homes");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }
}