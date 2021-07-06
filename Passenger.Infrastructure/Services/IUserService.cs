using System;
using Passenger.Infrastructure.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDTO> GetAsync(string email);
        
        Task RegisterAsync(Guid userId, string email,string username,string password,string role);

        Task LoginAsync(string email, string password);

        Task<IEnumerable<UserDTO>> BrowseAsync();


    }
}