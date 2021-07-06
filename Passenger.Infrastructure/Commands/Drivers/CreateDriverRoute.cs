using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriverRoute : AuthenticatedCommand
    {
        public string Name { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }

    }
}
