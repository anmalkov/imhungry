using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;

namespace ImHungry.Api.Tests;

class ImHungryApplication: WebApplicationFactory<Program>
{
    protected override IHostBuilder? CreateHostBuilder()
    {
        Environment.SetEnvironmentVariable("IMHUNGRY_CSV_DATA_URL", "https://data.sfgov.org//api//views//rqzj-sfat//rows.csv");
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
        var content = await reply.Content.ReadAsStringAsync();

        Assert.True(reply.IsSuccessStatusCode);
        Assert.Equal($"Food trucks nerby location {Places.Boutersem.Latitude}, {Places.Boutersem.Longitude}", await reply.Content.ReadAsStringAsync());
    }
}
