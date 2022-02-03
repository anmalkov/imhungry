using ImHungry.Api.Models;
using ImHungry.Api.Repositories;
using ImHungry.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Xunit;

namespace ImHungry.Api.Tests;

public class MobileFoodPointServiceTests
{

    [Fact]
    public async Task Get_Nearest()
    {
        var location = Places.Boutersem;
        var expectedPointsCount = 4;

        var repository = new Mock<IMobileFoodPointsRepository>();
        repository.Setup(r => r.GetAllAsync()).Returns(Task.FromResult<IEnumerable<MobileFoodPoint>>(new[] {
            new MobileFoodPoint { Id = 1, Location = new LatLng(location.Latitude + 10, location.Longitude + 10) },
            new MobileFoodPoint { Id = 2, Location = new LatLng(location.Latitude + 20, location.Longitude + 20) },
            new MobileFoodPoint { Id = 3, Location = new LatLng(location.Latitude + 30, location.Longitude + 30) },
            new MobileFoodPoint { Id = 4, Location = new LatLng(location.Latitude + 40, location.Longitude + 40) },
            new MobileFoodPoint { Id = 5, Location = new LatLng(location.Latitude + 50, location.Longitude + 50) },
            new MobileFoodPoint { Id = 6, Location = new LatLng(location.Latitude - 10, location.Longitude - 10) },
            new MobileFoodPoint { Id = 7, Location = new LatLng(location.Latitude - 20, location.Longitude - 20) },
            new MobileFoodPoint { Id = 8, Location = new LatLng(location.Latitude - 30, location.Longitude - 30) },
            new MobileFoodPoint { Id = 9, Location = new LatLng(location.Latitude - 40, location.Longitude - 40) },
            new MobileFoodPoint { Id = 10, Location = new LatLng(location.Latitude - 50, location.Longitude - 50) }
        }));
                
        var logger = new Mock<ILogger<MobileFoodPointsService>>();

        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        var service = new MobileFoodPointsService(repository.Object, logger.Object, memoryCache);

        var nearestPoints = await service.GetNearestAsync(location, expectedPointsCount);

        Assert.NotNull(nearestPoints);
        Assert.Equal(expectedPointsCount, nearestPoints.Count());
        Assert.Contains(nearestPoints, p => p.Id == 1);
        Assert.Contains(nearestPoints, p => p.Id == 2);
        Assert.Contains(nearestPoints, p => p.Id == 6);
        Assert.Contains(nearestPoints, p => p.Id == 7);
    }
}
