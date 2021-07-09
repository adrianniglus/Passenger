using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {

        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _routeService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService,IDriverService driverService, IDriverRouteService routeService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _routeService = routeService;
            _logger = logger;
        }

        public IUserService UserService { get; }

        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();

            if(users.Any())
            {
                return;
            }


            _logger.LogTrace("Initializing data...");

            var tasks = new List<Task>();

            for(var i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"userrr{i}"; 

                await _userService.RegisterAsync(userId,$"{username}@test.com",username, "Secret123456","user");
                await _driverService.CreateAsync(userId);
                await _driverService.SetVehicleAsync(userId, "BMW", "i8");
                await _routeService.AddAsync(userId, "Route name", 1, 1, 2, 2);
                await _routeService.AddAsync(userId, "Second route name", 1, 1, 2, 2);
            }

            for(var i = 1; i <= 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";

                await _userService.RegisterAsync(userId,$"{username}@test.com",username, "Secret123456","admin");
            }


            await Task.WhenAll(tasks);

            _logger.LogTrace("Data was initialized");
        }
    }
}