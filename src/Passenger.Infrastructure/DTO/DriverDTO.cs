using System;
using System.Collections.Generic;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDTO
    {
        public Guid UserId {get; set;}
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}