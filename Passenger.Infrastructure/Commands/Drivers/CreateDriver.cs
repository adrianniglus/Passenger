using System;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommand
    {
        public string VehicleBrand {get; set;}
        public string VehicleName {get; set;}
    }
}