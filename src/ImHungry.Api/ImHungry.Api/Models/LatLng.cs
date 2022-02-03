using System.Globalization;

namespace ImHungry.Api.Models;

public class LatLng
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public LatLng(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static bool TryParse(string? value, IFormatProvider? provider,
                                out LatLng? coordinate)
    {
        // Format is "latitude,longitude" => "50.830369184065056,4.831816576155524"
        var segments = value?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (segments?.Length == 2
            && double.TryParse(segments[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var latitude)
            && double.TryParse(segments[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var longitude))
        {
            coordinate = new LatLng(latitude, longitude);
            return true;
        }

        coordinate = null;
        return true;
    }
}
