using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public class AuthenticatedCommand : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
    }
}
