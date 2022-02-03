using ImHungry.Api.Models;

namespace ImHungry.Api.Repositories
{
    public interface IMobileFoodPointsRepository
    {
        Task<IEnumerable<MobileFoodPoint>> GetAllAsync();
    }
}
