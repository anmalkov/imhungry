using ImHungry.Api.Models;

namespace ImHungry.Api.Services;

public interface IMobileFoodPointsService
{
    Task<IEnumerable<MobileFoodPoint>> GetNearestAsync(LatLng location, int count);
}
