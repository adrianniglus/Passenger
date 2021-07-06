using Passenger.Infrastructure.DTO;
using System;

namespace Passenger.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDTO CreateToken(Guid userId, string role);
    }
}