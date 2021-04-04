using System;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public DriverDTO Get(Guid userId)
        {
            var driver = _driverRepository.Get(userId);

            return new DriverDTO
            {
                //UserId = driver.UserId
            };
        }
    }
}