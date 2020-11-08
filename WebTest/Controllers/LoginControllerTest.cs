using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models.Login;
using Moq;
using Service.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using Web;
using Web.Controllers;
using Web.Utils;
using WebTest.Helpers;
using Xunit;

namespace WebTest.Controllers
{
    public class LoginControllerTest
    {
        private LoginController _controller;
        private readonly Mock<ILoginService> _serviceMock;
        private readonly Mock<IUserLoginManager> _managerMock;
        private readonly Mock<IStringLocalizer<SharedResource>> _localizerMock;

        public LoginControllerTest()
        {
            _serviceMock = new Mock<ILoginService>();
            _managerMock = new Mock<IUserLoginManager>();
            _localizerMock = new Mock<IStringLocalizer<SharedResource>>();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        [InlineData("12345", "12345")]
        public async Task Should_Authenticate_ReturnsBadRequest(string email, string password)
        {
            //Arrange

            var model = new AuthenticateModel
            {
                Email = email,
                Password = password
            };

            _controller = new LoginController(_managerMock.Object, _serviceMock.Object, _localizerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Authenticate(model);

            //Assert

            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async Task Should_Authenticate_ReturnsNotFound()
        {
            //Arrange

            const string password = "12345";
            const string email = "someone@outlook.com";
            const string localizedMessage = "The email/password is incorrect. please try again.";

            var model = new AuthenticateModel
            {
                Email = email,
                Password = password
            };

            _managerMock.Setup(x => x.ExistsAsync(email, password))
                .ReturnsAsync(false);

            var localizedString = new LocalizedString(LocalizationMessage.EmailOrPasswordIsIncorrect, localizedMessage);

            _localizerMock.Setup(x => x[LocalizationMessage.EmailOrPasswordIsIncorrect])
                .Returns(localizedString);

            _controller = new LoginController(_managerMock.Object, _serviceMock.Object, _localizerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Authenticate(model);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(email, password), Times.Once);

            var badRequestObject = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal(localizedMessage, badRequestObject.Value);

        }

        [Fact]
        public async Task Should_Authenticate_ReturnsOk()
        {
            //Arrange

            const string token = "123";
            const string password = "12345";
            const string email = "someone@outlook.com";

            var model = new AuthenticateModel
            {
                Email = email,
                Password = password
            };

            var userModel = new UserLogin()
            {
                Email = email,
            };

            _managerMock.Setup(x => x.ExistsAsync(email, password))
                .ReturnsAsync(true);

            _managerMock.Setup(x => x.GetUserAsync(email, password))
                .ReturnsAsync(userModel);

            _serviceMock.Setup(x => x.BuildToken(userModel)).Returns(token);

            _controller = new LoginController(_managerMock.Object, _serviceMock.Object, _localizerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Authenticate(model);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(email, password), Times.Once);
            _managerMock.Verify(x => x.GetUserAsync(email, password), Times.Once);
            _serviceMock.Verify(x => x.BuildToken(userModel), Times.Once);

            var OkResult = Assert.IsType<OkObjectResult>(result);
            var tokenResult = Assert.IsAssignableFrom<string>(OkResult.Value);

            Assert.NotNull(token);
            Assert.Equal(token, tokenResult);

        }

        [Fact]
        public async Task Should_GetUserInfo_ReturnsOk()
        {
            //Arrange

            const int userId = 1;
            const string email = "someone@outlook.com";

            var userModel = new UserLogin()
            {
                Id = userId,
                Email = email,
            };

            var claimUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", userId.ToString())
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claimUser }
            };

            _managerMock.Setup(x => x.GetUserAsync(userId))
                .ReturnsAsync(userModel);

            _controller = new LoginController(_managerMock.Object, _serviceMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.GetUserInfo();

            //Assert

            _managerMock.Verify(x => x.GetUserAsync(userId), Times.Once);

            var OkResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsAssignableFrom<UserLogin>(OkResult.Value);

            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
            Assert.Equal(email, user.Email);
        }
    }
}
