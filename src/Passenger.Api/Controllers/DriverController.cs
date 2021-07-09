using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("drivers")]
    public class DriverController: ApiControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService, ICommandDispatcher commandDispatcher)
        :base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Logger.Info("Fetching drivers");
            var drivers = await _driverService.BrowseAsync();

            return Ok(drivers);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var driver = await _driverService.GetAsync(userId);
            if(driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriver command)
        {        
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new DeleteDriver());

            return NoContent();
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> Put([FromBody] UpdateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}