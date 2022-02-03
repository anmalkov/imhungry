using ImHungry.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

namespace ImHungry.Api.Tests;

class ImHungryApplication: WebApplicationFactory<Program>
{
    protected override IHostBuilder? CreateHostBuilder()
    {
        Environment.SetEnvironmentVariable("IMHUNGRY_CSV_DATA_URL", "https://data.sfgov.org/api/views/rqzj-sfat/rows.csv");
        return base.CreateHostBuilder();
    }

}

public class ApiTests
{
    [Fact]
    public async void Returns_Error()
    {
        await using var app = new ImHungryApplication();

        var client = app.CreateClient();

        var reply = await client.GetAsync($"/v1/37sds77638040110528");
        var content = await reply.Content.ReadAsStringAsync();

        Assert.Equal(400, (int)reply.StatusCode);
        Assert.Contains("\"error", content);
    }


    [Fact]
    public async void Returns_Nearest_Points()
    {
        await using var app = new ImHungryApplication();

        var client = app.CreateClient();

        var reply = await client.GetAsync($"/v1/37.77638040110528,-122.42590558289392");
        var points = await reply.Content.ReadFromJsonAsync<IEnumerable<MobileFoodPointDto>>();

        Assert.True(reply.IsSuccessStatusCode);
        Assert.NotNull(points);
        Assert.Equal(5, points.Count());
    }
}
