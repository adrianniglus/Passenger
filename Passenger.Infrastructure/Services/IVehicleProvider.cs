using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IVehicleProvider : IService
    {
        Task<IEnumerable<VehicleDTO>> BrowseAsync();
        Task<VehicleDTO> GetAsync(string brand, string name);
    }
}
