using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Residential;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;

namespace WebTest.Controllers
{
    public class ResidentialStatusControllerTest
    {
        private ResidentialStatusController _controller;
        private readonly Mock<IResidentialStatusManager> _managerMock;
        public ResidentialStatusControllerTest()
        {
            _managerMock = new Mock<IResidentialStatusManager>();
        }

        [Fact]
        public async Task Should_GetAll_ReturnsAListOfResidentialStatus()
        {
            //Arrange

            var statusModels = new List<ResidentialStatus>
            {
                new ResidentialStatus(),
                new ResidentialStatus(),
                new ResidentialStatus(),
            };

            _managerMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(statusModels);

            _controller = new ResidentialStatusController(_managerMock.Object);

            //Act

            var result = await _controller.GetAll();

            //Assert

            _managerMock.Verify(x => x.GetAllAsync(), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var dynamicModel = Assert.IsAssignableFrom<dynamic>(okResult.Value);
            Assert.NotNull(dynamicModel);

            IEnumerable<ResidentialStatus> models = dynamicModel.GetType()
                .GetProperty("status").GetValue(dynamicModel, null);

            Assert.NotNull(models);
            Assert.Equal(3, models.Count());
        }
    }
}
