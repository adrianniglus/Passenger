using System;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Passenger.Infrastructure.Exceptions;
using Passenger.Core.Domain;

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
                throw new ServiceException(Exceptions.ErrorCodes.InvalidCredentials,"Invalid credentials");
            }

            var salt = user.Salt;
            var hash = _encrypter.GetHash(password, salt);

            if(user.Password == hash)
            {
                return;
            }
            throw new ServiceException(Exceptions.ErrorCodes.InvalidCredentials,"Invalid credentials");
        }

        public async Task RegisterAsync(Guid userId, string email,string username, string password,string role)
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new ServiceException(Exceptions.ErrorCodes.EmailInUse,"Email already in use");
            }

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password,salt);
            user = new User(userId,email,username,hash,role,salt);
            await _userRepository.AddAsync(user);
            
        }

        public async Task<IEnumerable<UserDTO>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }
    }
}