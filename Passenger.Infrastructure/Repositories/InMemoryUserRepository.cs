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
            new User("User1@gmail.com","user1","secret","salt"),
            new User("User2@gmail.com","user2","secret","salt"),
            new User("User3@gmail.com","user3","secret","salt")
        };
        
        public void Add(User user)
        {
            _users.Add(user);
        }
        public User Get(Guid id)
            => _users.Single(x => x.Id == id);

        public User Get(string email)
            => _users.Single(x => x.Email == email.ToLowerInvariant());
            
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