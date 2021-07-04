using System;

namespace Passenger.Infrastructure.Commands.Users
{
    public class Login : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid TokenId { get; set; }
    }
}