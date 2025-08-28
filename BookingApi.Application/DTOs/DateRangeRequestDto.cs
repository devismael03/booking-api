using System.ComponentModel.DataAnnotations;

namespace BookingApi.Application.DTOs;

public class DateRangeRequestDto
{
    [Required]
    public string StartDate { get; set; } = string.Empty;
    
    [Required]
    public string EndDate { get; set; } = string.Empty;
}