using System;
using System.Collections.Generic;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDTO
    {
        public Guid UserId {get; protected set;}
        public Vehicle Vehicle {get; protected set;}
        public IEnumerable<Route> Routes {get; protected set;}
        public IEnumerable<DailyRoute> DailyRoutes {get; protected set;}
    }
}