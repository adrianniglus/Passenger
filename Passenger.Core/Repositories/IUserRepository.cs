using System;
using Passenger.Core.Domain;
using System.Collections.Generic;

namespace Passenger.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid id);
        User Get(string email);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Remove(Guid id);
        void Update(User user);
    }
}