using ImHungry.Api.Models;

namespace ImHungry.Api.Helpers;

public enum Unit
{
    Miles = 1,
    Kilometers
}

public static class GeoHelper
{
    public static double GetDistance(LatLng coordinate1, LatLng coordinate2, Unit unit = Unit.Kilometers)
    {
        var r = GetRadiusOfEarth(unit);
        var dLat = ToRadians(coordinate2.Latitude - coordinate1.Latitude);
        var dLon = ToRadians(coordinate2.Longitude - coordinate1.Longitude);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(coordinate1.Latitude)) * Math.Cos(ToRadians(coordinate2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var d = r * c;
        return d;
    }

    private static double GetRadiusOfEarth(Unit units)
    {
        // Radius of the earth in km 6371 and in miles 3958.75587
        return units == Unit.Kilometers ? 6371.0 : 3958.76;
    }

    private static double ToRadians(double degrees)
    {
        return (degrees * Math.PI) / 180;
    }
}
