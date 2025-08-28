using BookingApi.Application.DTOs;
using BookingApi.Application.Interfaces;
using BookingApi.Application.DataStructures;

namespace BookingApi.Application.Services;

public class HomeApplicationService : IHomeApplicationService
{
    private readonly IHomeRepository _homeRepository;
    private IntervalTree? _intervalTree;
    private Dictionary<string, Domain.Entities.Home>? _homesCache;
    private bool _isTreeInitialized = false;

    public HomeApplicationService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository ?? throw new ArgumentNullException(nameof(homeRepository));
    }

    public async Task<AvailableHomesResponseDto> GetAvailableHomesAsync(DateOnly startDate, DateOnly endDate)
    {
        if (!_isTreeInitialized)
        {
            await InitializeIntervalTree();
        }

        var availableHomeIds = _intervalTree!.SearchHomeIds(startDate, endDate);
        
        var filteredHomes = availableHomeIds
            .Where(homeId => _homesCache!.ContainsKey(homeId))
            .Select(homeId => _homesCache![homeId])
            .ToList();
        
        var homeDtos = filteredHomes.Select(MapToDto).ToList();
        
        return new AvailableHomesResponseDto
        {
            Status = "OK",
            Homes = homeDtos
        };
    }

    private async Task InitializeIntervalTree()
    {
        var allHomes = await _homeRepository.GetAllAsync();
        
        _intervalTree = new IntervalTree();
        _homesCache = new Dictionary<string, Domain.Entities.Home>();
        
        foreach (var home in allHomes)
        {
            _homesCache[home.HomeId] = home;
            
            var intervals = ConvertToIntervals(home.AvailableSlots, home.HomeId);
            
            foreach (var interval in intervals)
            {
                _intervalTree.Insert(interval);
            }
        }
        
        _isTreeInitialized = true;
    }

    private static List<Interval> ConvertToIntervals(List<DateOnly> availableDates, string homeId)
    {
        if (!availableDates.Any())
            return new List<Interval>();

        var sortedDates = availableDates.OrderBy(d => d).ToList();
        var intervals = new List<Interval>();
        
        var currentStart = sortedDates[0];
        var currentEnd = sortedDates[0];
        
        for (int i = 1; i < sortedDates.Count; i++)
        {
            if (sortedDates[i] == currentEnd.AddDays(1))
            {
                currentEnd = sortedDates[i];
            }
            else
            {
                intervals.Add(new Interval(currentStart, currentEnd, homeId));
                
                currentStart = sortedDates[i];
                currentEnd = sortedDates[i];
            }
        }
        
        intervals.Add(new Interval(currentStart, currentEnd, homeId));
        
        return intervals;
    }

    private static HomeDto MapToDto(Domain.Entities.Home home)
    {
        return new HomeDto
        {
            HomeId = home.HomeId,
            HomeName = home.HomeName,
            AvailableSlots = home.AvailableSlots.Select(date => date.ToString("yyyy-MM-dd")).ToList()
        };
    }
}