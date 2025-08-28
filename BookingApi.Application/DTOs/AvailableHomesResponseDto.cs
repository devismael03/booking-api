namespace BookingApi.Application.DTOs;

public class AvailableHomesResponseDto
{
    public string Status { get; set; } = "OK";
    public List<HomeDto> Homes { get; set; } = new();
}