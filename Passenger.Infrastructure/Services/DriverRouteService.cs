using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
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

        private readonly IRouteManager _routeManager;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, IRouteManager routeManager, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _routeManager = routeManager;
            _mapper = mapper;
        }
        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);

            var startAddress = await _routeManager.GetAddressAsync(startLatitude, startLongitude);
            var endAddress = await _routeManager.GetAddressAsync(endLatitude,endLongitude);

            var startNode = Node.Create(startAddress, startLongitude, startLatitude);
            var endNode = Node.Create(endAddress, endLongitude, endLatitude);

            var distance = _routeManager.CalculateDistance(startNode,endNode);

            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);


        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);

            driver.DeleteRoute(name);

            await _driverRepository.UpdateAsync(driver);
            
        }

        
    }
}
