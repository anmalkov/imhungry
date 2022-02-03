using ImHungry.Api.Helpers;
using ImHungry.Api.Models;
using ImHungry.Api.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace ImHungry.Api.Services;

public class MobileFoodPointsService : IMobileFoodPointsService
{
    private const string MobileFoodPointsCacheKey = "mobile-food-points";
    private const int CacheExpirationInHours = 12;

    private readonly IMobileFoodPointsRepository _mobileFoodPointsRepository;
    private readonly IMemoryCache _cache;
    private readonly ILogger<MobileFoodPointsService> _logger;

    public MobileFoodPointsService(IMobileFoodPointsRepository mobileFoodPointsRepository, ILogger<MobileFoodPointsService> logger, IMemoryCache memoryCache)
    {
        _mobileFoodPointsRepository = mobileFoodPointsRepository;
        _cache = memoryCache;
        _logger = logger;
    }

    public async Task<IEnumerable<MobileFoodPoint>> GetNearestAsync(LatLng location, int count)
    {
        var points = await GetAllMobileFoodPointsAsync();

        var pointsWithDistance = points
            .Select(p => new { p.Id, Distance = GeoHelper.GetDistance(location, p.Location, Unit.Miles) })
            .OrderBy(p => p.Distance)
            .Take(count)
            .ToList();

        var nearestPoints = new List<MobileFoodPoint>();
        foreach (var point in pointsWithDistance)
        {
            var p = points.First(p => p.Id == point.Id);
            p.Distance = point.Distance;
            nearestPoints.Add(p);
        }

        return nearestPoints;
    }

    private async Task<IEnumerable<MobileFoodPoint>> GetAllMobileFoodPointsAsync()
    {
        IEnumerable<MobileFoodPoint> points;
        if (!_cache.TryGetValue(MobileFoodPointsCacheKey, out points))
        {
            points = await _mobileFoodPointsRepository.GetAllAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(CacheExpirationInHours));
            _cache.Set(MobileFoodPointsCacheKey, points, cacheEntryOptions);
        }

        return points;
    }
}
