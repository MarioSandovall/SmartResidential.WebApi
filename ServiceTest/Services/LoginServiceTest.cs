using Microsoft.Extensions.Options;
using Model.Models.Login;
using Model.Settings;
using Moq;
using Service.Services;
using System.Collections.Generic;
using Xunit;

namespace ServiceTest.Services
{
    public class LoginServiceTest
    {
        private LoginService _service;
        private readonly Mock<IOptions<JwtConfiguration>> _jwtOptionsMock;
        public LoginServiceTest()
        {
            _jwtOptionsMock = new Mock<IOptions<JwtConfiguration>>();
        }

        [Fact]
        public void Should_BuildToken()
        {
            //Arrange

            var user = new UserLogin
            {
                Id = 1,
                Name = "User 1",
                Email = "someone@outlook.com",
                Roles = new List<string> { "Admin" }
            };

            var jwtConfigurations = new JwtConfiguration
            {
                Key = "this is a key very secret"
            };

            _jwtOptionsMock.Setup(x => x.Value)
                .Returns(jwtConfigurations);


            _service = new LoginService(_jwtOptionsMock.Object);

            //Act

            var token = _service.BuildToken(user);

            //Assert

            Assert.False(string.IsNullOrEmpty(token));
        }

    }
}
