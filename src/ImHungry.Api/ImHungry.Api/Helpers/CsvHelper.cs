using ImHungry.Api.Models;
using System.Globalization;

namespace ImHungry.Api.Helpers;

public static class CsvHelper
{
    public static IEnumerable<MobileFoodPoint> MapCsvData(string? csvData, ILogger logger)
    {
        var mobileFoodPoints = new List<MobileFoodPoint>();
        if (csvData is null)
        {
            return mobileFoodPoints;
        }

        var reader = new StringReader(csvData);
        // get rid of the first line that contains column names
        var _ = reader.ReadLine();
        var id = 0;
        var latitude = 0.0;
        var longitude = 0.0;
        while (true)
        {
            var line = reader.ReadLine();
            if (line == null)
            {
                break;
            }

            var lineData = line.Split(',');
            try
            {
                id = int.Parse(lineData[0]);
                latitude = double.Parse(lineData[14], CultureInfo.InvariantCulture);
                longitude = double.Parse(lineData[15], CultureInfo.InvariantCulture);
            }
            catch
            {
                logger.LogDebug($"Item with Id '{lineData[0]}' skipped because of error. Latitude: '{lineData[14]}', Longitude: '{lineData[15]}'");
                continue;
            }

            if (latitude == 0.0 || longitude == 0.0)
            {
                logger.LogDebug($"Item with Id '{id}' skipped because of not correct values for latitude '{latitude}' or longitude '{longitude}'");
                continue;
            }

            mobileFoodPoints.Add(new MobileFoodPoint
            {
                Id = id,
                Name = lineData[1],
                Type = lineData[2],
                LocationDescription = lineData[4],
                Address = lineData[5],
                Food = lineData[11],
                Latitude = latitude,
                Longitude = longitude
            });
        }

        logger.LogDebug($"Mapped {mobileFoodPoints.Count} mobile food points from CSV date");

        return mobileFoodPoints;
    }

}
