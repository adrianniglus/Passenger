using System;
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
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();


            var userService = new UserService(userRepositoryMock.Object,encrypterMock.Object,mapperMock.Object);
            await userService.RegisterAsync(Guid.NewGuid(),"user1@test.com","user123456","Secret123456","user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Once);
        }
    }
}