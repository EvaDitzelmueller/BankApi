using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace BankingSystemAPITests.cs
{
    // This is an outline of a basic test to verify the response from the API using a test host client, needs proper setup with startup though
    public class APITests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public APITests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ApiIsUpAndRunning()
        {
            // Act
            var response = await _client.GetAsync("/");
            ;
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
