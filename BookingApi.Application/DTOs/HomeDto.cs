namespace BookingApi.Application.DTOs;

public class HomeDto
{
    public string HomeId { get; set; } = string.Empty;
    public string HomeName { get; set; } = string.Empty;
    public List<string> AvailableSlots { get; set; } = new();
}