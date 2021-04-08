using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Services;
using Xunit;


namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Test()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();


            var userService = new UserService(userRepositoryMock.Object,mapperMock.Object);
            await userService.RegisterAsync("user@email.com","user123","secreT123");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Once);
        }
    }
}