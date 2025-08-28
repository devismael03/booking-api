namespace BookingApi.Domain.Entities;

public class Home
{
    public Home(string homeId, string homeName, List<DateOnly> availableSlots)
    {
        HomeId = homeId ?? throw new ArgumentNullException(nameof(homeId));
        HomeName = homeName ?? throw new ArgumentNullException(nameof(homeName));
        AvailableSlots = availableSlots ?? throw new ArgumentNullException(nameof(availableSlots));
    }

    public string HomeId { get; private set; }
    public string HomeName { get; private set; }
    public List<DateOnly> AvailableSlots { get; private set; }

    public void AddAvailableSlot(DateOnly date)
    {
        if (!AvailableSlots.Contains(date))
        {
            AvailableSlots.Add(date);
        }
    }

    public void RemoveAvailableSlot(DateOnly date)
    {
        AvailableSlots.Remove(date);
    }

    public bool IsAvailableOn(DateOnly date)
    {
        return AvailableSlots.Contains(date);
    }
}