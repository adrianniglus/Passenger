using System;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using AutoMapper;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository,IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public DriverDTO Get(Guid userId)
        {
            var driver = _driverRepository.Get(userId);

            return _mapper.Map<Driver,DriverDTO>(driver);
        }
    }
}