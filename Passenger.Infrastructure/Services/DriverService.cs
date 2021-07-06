using System;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Passenger.Infrastructure.Settings;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleProvider _vehicleProvider;
        private readonly IMapper _mapper;


        public DriverService(
            IDriverRepository driverRepository,
            IUserRepository userRepository,
            IVehicleProvider vehicleProvider,
            IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
            _mapper = mapper;
        }

        public async Task<DriverDetailsDTO> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<Driver,DriverDetailsDTO>(driver);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception("User does not exist");
            }
            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new Exception("Driver already exist");
            }

            driver = new Driver(user);

            await _driverRepository.AddAsync(driver);

        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception("Driver wasn't found");
            }
            var vehicleDetails = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(brand, name, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task<IEnumerable<DriverDTO>> BrowseAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDTO>>(drivers);
        }
    }
}