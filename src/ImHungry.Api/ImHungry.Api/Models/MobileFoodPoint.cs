namespace ImHungry.Api.Models;

public class MobileFoodPoint
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? LocationDescription { get; set; }
    public string? Address { get; set; }

    public string? Food { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
