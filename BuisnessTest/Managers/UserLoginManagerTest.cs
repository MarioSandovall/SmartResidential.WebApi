using AutoMapper;
using Business.Managers;
using Domain.Entities;
using Model.Models.Login;
using Moq;
using Repository.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace BuisnessTest.Managers
{
    public class UserLoginManagerTest
    {
        private UserLoginManager _manager;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserLoginRepository> _repositoryMock;
        public UserLoginManagerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IUserLoginRepository>();
        }

        [Fact]
        public async Task Should_Call_ExistAsyncMethod()
        {
            //Arrange

            const string email = "someone@outlook.com";
            const string password = "12345";

            _manager = new UserLoginManager(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await _manager.ExistsAsync(email, password);

            //Assert

            _repositoryMock.Verify(x => x.ExistsAsync(email, password), Times.Once);
        }


        [Fact]
        public async Task Should_GetUserAsync_ResturnsALoginUserModelByEmailAndPassword()
        {
            //Arrange

            const string password = "12345";
            const string email = "someone@outloo.com";

            var userEntity = new UserEf
            {
                Email = email,
                Password = password
            };

            var userModel = new UserLogin
            {
                Email = email
            };

            _repositoryMock.Setup(x => x.GetUserAsync(email, password))
                .ReturnsAsync(userEntity);

            _mapperMock.Setup(x => x.Map<UserLogin>(userEntity))
                .Returns(userModel);

            _manager = new UserLoginManager(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var model = await _manager.GetUserAsync(email, password);

            //Assert

            _repositoryMock.Verify(x => x.GetUserAsync(email, password), Times.Once);
            _mapperMock.Verify(x => x.Map<UserLogin>(userEntity), Times.Once);

            Assert.NotNull(model);
            Assert.Equal(email, model.Email);

        }

        [Fact]
        public async Task Should_GetUserAsync_ResturnsALoginUserModelById()
        {
            //Arrange

            const int userId = 1;
            const string password = "12345";
            const string email = "someone@outlook.com";

            var userEntity = new UserEf
            {
                Id = userId,
                Email = email,
                Password = password
            };

            var userModel = new UserLogin
            {
                Id = userId,
                Email = email
            };

            _repositoryMock.Setup(x => x.GetUserAsync(userId))
                .ReturnsAsync(userEntity);

            _mapperMock.Setup(x => x.Map<UserLogin>(userEntity))
                .Returns(userModel);

            _manager = new UserLoginManager(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var model = await _manager.GetUserAsync(userId);

            //Assert

            _repositoryMock.Verify(x => x.GetUserAsync(userId), Times.Once);
            _mapperMock.Verify(x => x.Map<UserLogin>(userEntity), Times.Once);

            Assert.NotNull(model.Email);
            Assert.Equal(userId, model.Id);
            Assert.Equal(email, model.Email);

        }
    }
}
