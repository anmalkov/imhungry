using ImHungry.Api.Models;

namespace ImHungry.Api.Helpers
{
    public static class Mapper
    {
        public static MobileFoodPointDto MapToDto(MobileFoodPoint entity)
        {
            return new MobileFoodPointDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Food = entity.Food,
                LocationDescription = entity.LocationDescription,
                Type = entity.Type,
                Latitude = entity.Location.Latitude,
                Longitude = entity.Location.Longitude,
                Distance = entity.Distance
            };
        }

        public static IEnumerable<MobileFoodPointDto> MapToDto(IEnumerable<MobileFoodPoint> entities)
        {
            return entities.Select(e => MapToDto(e)).ToList();
        }
    }
}
