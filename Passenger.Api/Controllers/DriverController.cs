using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class DriverController: ApiControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService, ICommandDispatcher commandDispatcher)
        :base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet("{UserId}")]
       public async Task<IActionResult> Get(Guid userId)
       {
            var driver = await _driverService.GetAsync(userId);

            if(driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
       }
    
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateDriver command)
        {        
            await CommandDispatcher.DispatchAsync(command);

            return Created($"driver/{command.UserId}", new object());
        }   
    }
}