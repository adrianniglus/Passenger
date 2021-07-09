using AutoMapper;
using Passenger.Infrastructure.DTO;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => 
                {
                    cfg.CreateMap<User,UserDTO>();
                    cfg.CreateMap<Driver,DriverDTO>();
                    cfg.CreateMap<Driver, DriverDetailsDTO>();
                    cfg.CreateMap<Vehicle, VehicleDTO>();
                    cfg.CreateMap<Node, NodeDTO>();
                    cfg.CreateMap<Route, RouteDTO>();
                })
                .CreateMapper();
    }
}