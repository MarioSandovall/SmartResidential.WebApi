using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Models;
using Model.Models.House;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web;
using Web.Controllers;
using Web.Utils;
using WebTest.Helpers;
using Xunit;

namespace WebTest.Controllers
{
    public class HouseControllerTest
    {
        private HouseController _controller;
        private readonly Mock<IUserManager> _userManagerMock;
        private readonly Mock<IHouseManager> _houseManagerMock;
        private readonly Mock<IResidentialManager> _residentialManagerMock;
        private readonly Mock<IStringLocalizer<SharedResource>> _localizerMock;
        public HouseControllerTest()
        {
            _userManagerMock = new Mock<IUserManager>();
            _houseManagerMock = new Mock<IHouseManager>();
            _residentialManagerMock = new Mock<IResidentialManager>();
            _localizerMock = new Mock<IStringLocalizer<SharedResource>>();
        }

        [Fact]
        public async Task Should_GetAll_ReturnsAListOfHouses()
        {
            //Arrange

            const int userId = 1;
            const int residentialId = 1;
            const int totalOfItems = 10;

            var parameter = new Parameter();

            var models = new List<House>()
            {
                new House(),
                new House(),
                new House(),
                new House(),
                new House()
            };

            var paginatedList = new PaginatedList<House>(models, totalOfItems);

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(true);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(true);

            _houseManagerMock.Setup(x => x.GetAllAsync(residentialId, parameter))
                .ReturnsAsync(paginatedList);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.GetAll(residentialId, parameter);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            _houseManagerMock.Verify(x => x.GetAllAsync(residentialId, parameter), Times.Once);


            var OkResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<House>>(OkResult.Value);

            Assert.NotNull(model);
            Assert.Equal(10, model.TotalOfItems);
            Assert.Equal(5, model.Items.Count());
        }

        [Fact]
        public async Task Should_GetAll_ReturnsAForbidResult()
        {
            //Arrange

            const int userId = 1;
            const int residentialId = 1;

            var parameter = new Parameter();

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(false);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(true);

            var localizedString = new LocalizedString(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource,
                LocalizationMessage.YouDoNotHavePermissionToAccessThisResource);

            _localizerMock.Setup(x => x[LocalizationMessage.YouDoNotHavePermissionToAccessThisResource])
                .Returns(localizedString);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.GetAll(residentialId, parameter);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Never);

            _houseManagerMock.Verify(x => x.GetAllAsync(residentialId, parameter), Times.Never);


            var forbidResult = Assert.IsType<ForbidResult>(result);


            Assert.NotNull(forbidResult);
            Assert.True(forbidResult.AuthenticationSchemes.Contains(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
        }

        [Fact]
        public async Task Should_GetAll_ReturnsANotFoundResult()
        {
            //Arrange

            const int userId = 1;
            const int residentialId = 1;

            var parameter = new Parameter();

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(true);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(false);

            var localizedString = new LocalizedString(LocalizationMessage.ResidentialNotFound,
                LocalizationMessage.ResidentialNotFound);

            _localizerMock.Setup(x => x[LocalizationMessage.ResidentialNotFound])
                .Returns(localizedString);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.GetAll(residentialId, parameter);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            _houseManagerMock.Verify(x => x.GetAllAsync(residentialId, parameter), Times.Never);


            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);


            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.Value, LocalizationMessage.ResidentialNotFound);
        }

        [Fact]
        public async Task Should_Get_ReturnsAHouseModel()
        {
            //Arrange

            const int userId = 2;
            const int houseId = 1;
            const int residentialId = 3;

            var houseModel = new HouseToUpdate
            {
                Id = houseId
            };

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(true);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(true);

            _houseManagerMock.Setup(x => x.ExistsAsync(residentialId, houseId))
               .ReturnsAsync(true);

            _houseManagerMock.Setup(x => x.GetByIdAsync(houseId))
                .ReturnsAsync(houseModel);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.SuperAdmin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.Get(residentialId, houseId);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            _houseManagerMock.Verify(x => x.ExistsAsync(residentialId, houseId), Times.Once);

            _houseManagerMock.Verify(x => x.GetByIdAsync(houseId), Times.Once);

            var OkResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<HouseToUpdate>(OkResult.Value);

            Assert.NotNull(model);
            Assert.Equal(houseId, model.Id);
        }

        [Fact]
        public async Task Should_Get_ReturnsAForbidResult()
        {
            //Arrange

            const int userId = 2;
            const int houseId = 1;
            const int residentialId = 3;

            var houseModel = new HouseToUpdate
            {
                Id = houseId
            };

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(false);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(true);


            _houseManagerMock.Setup(x => x.ExistsAsync(residentialId, houseId))
               .ReturnsAsync(true);

            _houseManagerMock.Setup(x => x.GetByIdAsync(houseId))
                .ReturnsAsync(houseModel);

            var localizedString = new LocalizedString(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource,
            LocalizationMessage.YouDoNotHavePermissionToAccessThisResource);

            _localizerMock.Setup(x => x[LocalizationMessage.YouDoNotHavePermissionToAccessThisResource])
                .Returns(localizedString);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.Get(residentialId, houseId);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Never);

            _houseManagerMock.Verify(x => x.ExistsAsync(residentialId, houseId), Times.Never);

            _houseManagerMock.Verify(x => x.GetByIdAsync(houseId), Times.Never);

            var forbidResult = Assert.IsType<ForbidResult>(result);

            Assert.NotNull(forbidResult);
            Assert.True(forbidResult.AuthenticationSchemes.Contains(LocalizationMessage.YouDoNotHavePermissionToAccessThisResource));
        }

        [Fact]
        public async Task Should_Get_ReturnsANotFoundResultBecauseResidentialDoesNotExist()
        {
            //Arrange

            const int userId = 2;
            const int houseId = 1;
            const int residentialId = 3;

            var houseModel = new HouseToUpdate
            {
                Id = houseId
            };

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(true);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(false);

            _houseManagerMock.Setup(x => x.ExistsAsync(residentialId, houseId))
               .ReturnsAsync(true);

            _houseManagerMock.Setup(x => x.GetByIdAsync(houseId))
                .ReturnsAsync(houseModel);

            var localizedString = new LocalizedString(LocalizationMessage.ResidentialNotFound,
            LocalizationMessage.ResidentialNotFound);

            _localizerMock.Setup(x => x[LocalizationMessage.ResidentialNotFound])
                .Returns(localizedString);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.Get(residentialId, houseId);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            _houseManagerMock.Verify(x => x.ExistsAsync(residentialId, houseId), Times.Never);

            _houseManagerMock.Verify(x => x.GetByIdAsync(houseId), Times.Never);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.Value, LocalizationMessage.ResidentialNotFound);
        }

        [Fact]
        public async Task Should_Get_ReturnsANotFoundResultBecauseHouseDoesNotExist()
        {
            //Arrange

            const int userId = 2;
            const int houseId = 1;
            const int residentialId = 3;

            var houseModel = new HouseToUpdate
            {
                Id = houseId
            };

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(true);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(true);

            _houseManagerMock.Setup(x => x.ExistsAsync(residentialId, houseId))
               .ReturnsAsync(false);

            _houseManagerMock.Setup(x => x.GetByIdAsync(houseId))
                .ReturnsAsync(houseModel);

            var localizedString = new LocalizedString(LocalizationMessage.HouseNotFound,
            LocalizationMessage.HouseNotFound);

            _localizerMock.Setup(x => x[LocalizationMessage.HouseNotFound])
                .Returns(localizedString);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            //Act

            var result = await _controller.Get(residentialId, houseId);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            _houseManagerMock.Verify(x => x.ExistsAsync(residentialId, houseId), Times.Once);

            _houseManagerMock.Verify(x => x.GetByIdAsync(houseId), Times.Never);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(notFoundObjectResult.Value, LocalizationMessage.HouseNotFound);
        }

        [Fact]
        public async Task Should_Add_ReturnsANotContentResult()
        {
            //Arrange

            const int userId = 2;
            const int residentialId = 3;

            var houseModel = new HouseToAdd
            {
                Name = "House Name",
                Street = "Street Name",
                ResidentialId = residentialId
            };

            _userManagerMock.Setup(x => x.IsAdminAsync(residentialId, userId))
             .ReturnsAsync(true);

            _residentialManagerMock.Setup(x => x.ExistsAsync(residentialId))
               .ReturnsAsync(true);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(Business.Utils.ClaimsCustomTypes.Id, userId + ""),
                new Claim(Business.Utils.ClaimsCustomTypes.RoleId, ((int)RoleEnum.Admin) + "")
            }));

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object)
            {
                ControllerContext = controllerContext
            };

            _controller.ValidateViewModel(houseModel);

            //Act

            var result = await _controller.Add(residentialId, houseModel);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Once);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            _houseManagerMock.Verify(x => x.AddAsync(houseModel), Times.Once);

        }

        [Fact]
        public async Task Should_Add_ReturnsABadRequestResult()
        {
            //Arrange

            const int userId = 2;
            const int residentialId = 3;

            var houseModel = new HouseToAdd();

            _controller = new HouseController(_userManagerMock.Object, _houseManagerMock.Object,
                _residentialManagerMock.Object, _localizerMock.Object);

            _controller.ValidateViewModel(houseModel);

            //Act

            var result = await _controller.Add(residentialId, houseModel);

            //Assert

            _userManagerMock.Verify(x => x.IsAdminAsync(residentialId, userId), Times.Never);

            _residentialManagerMock.Verify(x => x.ExistsAsync(residentialId), Times.Never);

            _houseManagerMock.Verify(x => x.AddAsync(houseModel), Times.Never);

            Assert.IsType<BadRequestResult>(result);

        }
    }
}
