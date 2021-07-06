using System;
using System.Linq;
using System.Collections.Generic;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System.Threading.Tasks;


namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();

        public async Task AddAsync(Driver driver)
        {
            _drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task<Driver> GetAsync(Guid id)
            => await Task.FromResult(_drivers.SingleOrDefault(x => x.UserId == id));

        public async Task<IEnumerable<Driver>> GetAllAsync()
            => await Task.FromResult(_drivers);

        public async Task UpdateAsync(Driver driver)
        {
            //do something
            await Task.CompletedTask;
        }
    }
}