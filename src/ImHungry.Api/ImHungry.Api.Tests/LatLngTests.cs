using ImHungry.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ImHungry.Api.Tests;

public class LatLngTests
{
    public static IEnumerable<object[]> Parse_Coordinates_Data =>
        new List<object[]>
        {
            new object[] { "0,0", new LatLng(0, 0) },
            new object[] { "0, 0", new LatLng(0, 0) },
            new object[] { "0 , 0", new LatLng(0, 0) },
            new object[] { "0.0,0.0", new LatLng(0, 0) },
            new object[] { "0.0, 0.0", new LatLng(0, 0) },
            new object[] { "0.0 , 0.0", new LatLng(0, 0) },
            new object[] { "50.830369184065056,4.831816576155524", Places.Boutersem },
            new object[] { "50.830369184065056, 4.831816576155524", Places.Boutersem },
            new object[] { "50.830369184065056 , 4.831816576155524", Places.Boutersem }
        };

    [Theory]
    [MemberData(nameof(Parse_Coordinates_Data))]
    public void Parse_Coordinates(string coordinate, LatLng expectedCoordinate)
    {
        var result = LatLng.TryParse(coordinate, null, out LatLng resultCoordinate);

        Assert.True(result);
        Assert.Equal(expectedCoordinate.Latitude, resultCoordinate.Latitude, 15);
        Assert.Equal(expectedCoordinate.Longitude, resultCoordinate.Longitude, 15);
    }
}
