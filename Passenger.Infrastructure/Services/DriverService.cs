using System;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Passenger.Infrastructure.Settings;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;


        public DriverService(
            IDriverRepository driverRepository,
            IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task<DriverDTO> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<Driver,DriverDTO>(driver);
        }

        public async Task CreateDriver(Guid userId,string brand, string name, int seats)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if(driver != null && driver.UserId == userId)
            {
                throw new Exception("Driver alrady exists");
            }

            driver = new Driver(userId, brand, name, seats);
            await _driverRepository.AddAsync(driver);
        }
    }
}