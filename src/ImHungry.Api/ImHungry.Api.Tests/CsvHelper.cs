using Microsoft.Extensions.Logging;
using ImHungry.Api.Helpers;
using Moq;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ImHungry.Api.Tests;

public class CsvHelperTests
{
    [Fact]
    public async Task Map_MobileFoodPoints_From_CSVAsync()
    {
        var csvData = await File.ReadAllTextAsync("testdata.csv");
        var logger = new Mock<ILogger>();

        var mobileFoodPoints = CsvHelper.MapCsvData(csvData, logger.Object);

        Assert.Equal(552, mobileFoodPoints.Count());

        Assert.Equal(37.76201920035647, mobileFoodPoints.First().Latitude, 14);
        Assert.Equal(-122.42730642251331, mobileFoodPoints.First().Longitude, 14);

        Assert.Equal(37.762476819591, mobileFoodPoints.Last().Latitude, 12);
        Assert.Equal(-122.41181900367852, mobileFoodPoints.Last().Longitude, 14);
    }
}
