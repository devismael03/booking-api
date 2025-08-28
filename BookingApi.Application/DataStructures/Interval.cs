namespace BookingApi.Application.DataStructures;

public class Interval
{
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public string HomeId { get; set; } = string.Empty;

    public Interval(DateOnly start, DateOnly end, string homeId)
    {
        Start = start;
        End = end;
        HomeId = homeId;
    }

    public bool Contains(DateOnly queryStart, DateOnly queryEnd)
    {
        return Start <= queryStart && End >= queryEnd;
    }
}