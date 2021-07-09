using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IRouteManager : IService
    {
        Task<string> GetAddressAsync(double latitude, double longitude);
        double CalculateDistance(Node startNode, Node endNode);
    }
}
