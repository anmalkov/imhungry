using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace ImHungry.Api.Tests
{
    class ImHungryApplication: WebApplicationFactory<Program>
    { }

    public class ApiTests
    {
        [Fact]
        public async void Returns_Distance()
        {
            await using var app = new ImHungryApplication();

            var client = app.CreateClient();

            var reply = await client.GetAsync("/v1");

            Assert.True(reply.IsSuccessStatusCode);
            Assert.Equal("Food trucks", await reply.Content.ReadAsStringAsync());
        }
    }
}