using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RouteManager : IRouteManager
    {
        private static readonly Random Random = new Random();
       
        public async Task<string> GetAddressAsync(double latitude, double longitude)
            => await Task.FromResult($"Sample address {Random.Next(100)}");

        public double CalculateDistance(Node startNode, Node endNode)
           => Random.Next(500, 10000);
    }
}
