using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Commands.Users;
using Newtonsoft;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;

namespace Passenger.Api.Controllers
{
    
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly GeneralSettings _settings;
        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, GeneralSettings settings)
        :base(commandDispatcher)
        {
            _settings = settings;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();

            return Ok(users);
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
            
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {        
            await DispatchAsync(command);

            return Created($"users/{command.Email}", null);
        }

            
    }
}
