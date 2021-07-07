using Passenger.Infrastructure.Commands.Drivers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class UpdateDriver : AuthenticatedCommand
    {
        public DriverVehicle Vehicle { get; set; }
    }
}
