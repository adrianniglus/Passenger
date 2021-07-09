using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;
        public LoginController(ICommandDispatcher commandDispatcher,IMemoryCache cache): base(commandDispatcher)
        {
            _cache = cache;
        }

        protected async Task<IActionResult> Post([FromBody]Login command)
        {
            command.TokenId = Guid.NewGuid();

            await DispatchAsync(command);

            var jwt = _cache.GetJwt(command.TokenId);

            return Ok(jwt);
        }
    }
}