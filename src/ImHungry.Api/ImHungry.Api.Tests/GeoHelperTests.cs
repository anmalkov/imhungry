using ImHungry.Api.Helpers;
using ImHungry.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ImHungry.Api.Tests;

public class GeoHelperTests
{
    public static IEnumerable<object[]> Calculate_Distance_Data =>
        new List<object[]>
        {
            new object[] { Places.Boutersem, Places.Boutersem, Unit.Kilometers, 0 },
            new object[] { Places.Boutersem, Places.Boutersem, Unit.Miles, 0 },
            new object[] { Places.Boutersem, Places.Kyiv, Unit.Kilometers, 1802.368, 3 },
            new object[] { Places.Boutersem, Places.Kyiv, Unit.Miles, 1119.941, 3 }
        };

    [Theory]
    [MemberData(nameof(Calculate_Distance_Data))]
    public void Calculate_Distance(LatLng coordinate1, LatLng coordinate2, Unit unit, double expectedDistance, int precision = 0)
    {
        var distance = GeoHelper.GetDistance(coordinate1, coordinate2, unit);

        Assert.Equal(expectedDistance, distance, precision);
    }
}
