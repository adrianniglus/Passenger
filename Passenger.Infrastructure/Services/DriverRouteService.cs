using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {

        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = GetDriver(userId);

            var startNode = Node.Create("Start address", startLongitude, startLatitude);
            var endNode = Node.Create("End address", endLongitude, endLatitude);
            var route = Route.Create(name, startNode, endNode);

            driver.AddRoute(name, startNode, endNode);
            await _driverRepository.UpdateAsync(driver);


        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = GetDriver(userId);

            driver.DeleteRoute(name);

            await _driverRepository.UpdateAsync(driver);
            
        }

        public Driver GetDriver(Guid userId)
        {
            var driver = _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user id: {userId} does not exist!");
            }
            return driver.Result;
        }
    }
}
