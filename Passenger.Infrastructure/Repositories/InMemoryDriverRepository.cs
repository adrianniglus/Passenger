using System;
using System.Linq;
using System.Collections.Generic;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;


namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();

        public void Add(Driver driver)
        {
            _drivers.Add(driver);
        }

        public Driver Get(Guid id)
            => _drivers.Single(x => x.UserId == id);

        public IEnumerable<Driver> GetAll()
            => _drivers;

        public void Update(Driver driver)
        {
            //do something
        }
    }
}