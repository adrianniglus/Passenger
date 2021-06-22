using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid UserId {get; protected set;}
        public Vehicle Vehicle {get; protected set;}
        public IEnumerable<Route> Routes {get; protected set;}
        public IEnumerable<DailyRoute> DailyRoutes {get; protected set;}
        public DateTime UpdatedAt {get; protected set;}

        public Driver()
        {
        }

        public Driver(Guid userId, string brand, string name, int seats)
        {
            UserId = userId;
            AddVehicle(brand,name,seats);
        }

        public void AddVehicle(string brand, string name, int seats)
        {
            Vehicle = Vehicle.Create(brand,name,seats);
        }
    }
}