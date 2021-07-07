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

        //[fact]
        //public async task given_unique_id_driver_should_be_created()
        //{
        //    var command = new createdriver
        //    {
        //        userid = guid.newguid(),
        //        vehiclebrand = "toyota",
        //        vehiclename = "supra"
        //    };

        //    var payload = getpayload(command);

        //    var response = await client.postasync("driver",payload);

        //    response.statuscode.should().beequivalentto(httpstatuscode.created);
        //    response.headers.location.tostring().should().beequivalentto($"driver/{command.userid}");
        //}
    }
}