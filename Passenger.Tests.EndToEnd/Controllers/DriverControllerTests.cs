using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class DriverControllerTests : ControllerTestsBase
    {
        
        [Fact]
        public async Task given_unique_id_driver_should_be_created()
        {
            var command = new CreateDriver
            {
                UserId = Guid.NewGuid(),
                VehicleBrand = "Toyota",
                VehicleName = "Supra",
                VehicleSeats = 5
            };

            var payload = GetPayload(command);

            var response = await Client.PostAsync("driver",payload);
            
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"driver/{command.UserId}");
        }
    }
}