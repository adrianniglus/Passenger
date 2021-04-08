using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Passenger.Api;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Passenger.Infrastructure.DTO;
using FluentAssertions;
using System.Net;
using System.Text;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public UsersControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                        .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            var email = "user1@gmail.com";
            var response = await _client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<UserDTO>(responseString);

            user.Email.Should().BeEquivalentTo(email);
        }
        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "use@gmail.com";
            var response = await _client.GetAsync($"users/{email}");
            
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            var request = new CreateUser
            {
                Email = "uniqueuser@email.com",
                Username = "uniqueUser",
                Password = "Secret1346"
            };

            var payload = GetPayload(request);

            var response = await _client.PostAsync("users",payload);
            
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"users/{request.Email}");
        }
        
        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8,"application/json");
        }
    }
}