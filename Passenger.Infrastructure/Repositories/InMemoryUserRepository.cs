using System;
using System.Linq;
using System.Collections.Generic;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
            new User("User1@gmail.com","user123","Secret123","salt"),
            new User("User2@gmail.com","user234","Secret123","salt"),
            new User("User3@gmail.com","user356","Secret123","salt")
        };
        
        public void Add(User user)
        {
            _users.Add(user);
        }
        public User Get(Guid id)
            => _users.SingleOrDefault(x => x.Id == id);

        public User Get(string email)
            => _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
            
        public IEnumerable<User> GetAll()
            => _users;

        public void Remove(Guid id)
        {
            var user = Get(id);
            _users.Remove(user);
        }

        public void Update(User user)
        {
            //do something
        }
    }
}