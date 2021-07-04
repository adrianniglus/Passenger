using System;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using AutoMapper;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<User,UserDTO>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if(user == null)
            {
                throw new Exception("Wrong login data!");
            }

            var salt = user.Salt;
            var hash = _encrypter.GetHash(password, salt);

            if(user.Password == hash)
            {
                return;
            }
            throw new Exception("Wrong login data");
        }

        public async Task RegisterAsync(Guid userId, string email,string username, string password,string role)
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception("Email already in use");
            }

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password,salt);
            user = new User(userId,email,username,hash,role,salt);
            await _userRepository.AddAsync(user);
            
        }
    }
}