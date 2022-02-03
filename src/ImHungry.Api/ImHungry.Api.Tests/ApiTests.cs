using Microsoft.AspNetCore.Mvc.Testing;
using System.Globalization;
using System.Net.Http;
using Xunit;

namespace ImHungry.Api.Tests;

class ImHungryApplication: WebApplicationFactory<Program>
{ }

public class ApiTests
{
    [Fact]
    public async void Returns_Distance()
    {
        await using var app = new ImHungryApplication();

        var client = app.CreateClient();

        var reply = await client.GetAsync($"/v1/50.830369184065056,4.831816576155524");

        Assert.True(reply.IsSuccessStatusCode);
        Assert.Equal($"Food trucks nerby location {Places.Boutersem.Latitude}, {Places.Boutersem.Longitude}", await reply.Content.ReadAsStringAsync());
    }
}
