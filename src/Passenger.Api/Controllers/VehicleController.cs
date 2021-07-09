using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    public class VehicleController : ApiControllerBase
    {

        private readonly IVehicleProvider _vehicleProvider;
        public VehicleController(ICommandDispatcher commandDispatcher,
            IVehicleProvider vehicleProvider)
        :base(commandDispatcher)
        {
            _vehicleProvider = vehicleProvider;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicle = await _vehicleProvider.BrowseAsync();

            return Ok(vehicle);
        }
    }
}
