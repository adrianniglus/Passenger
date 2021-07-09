using Passenger.Infrastructure.Commands.Drivers.Models;
using System;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommand
    {
        public DriverVehicle Vehicle { get; set; }
    }
}