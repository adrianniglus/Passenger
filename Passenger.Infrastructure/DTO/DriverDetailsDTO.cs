using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDetailsDTO : DriverDTO
    {
        public IEnumerable<RouteDTO> Routes { get; set; }
    }
}
