using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.DTO
{
    public class RouteDTO
    {
        public string Name { get; set; }
        public NodeDTO StartNode { get; set; }
        public NodeDTO EndNode { get; set; }
        public double Distance { get; set; }

    }
}
