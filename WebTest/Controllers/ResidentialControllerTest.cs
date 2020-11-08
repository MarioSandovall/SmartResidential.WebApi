using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Models.Residential;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using WebTest.Helpers;
using Xunit;

namespace WebTest.Controllers
{
    public class ResidentialControllerTest
    {
        private ResidentialController _controller;
        private readonly Mock<IResidentialManager> _managerMock;
        public ResidentialControllerTest()
        {
            _managerMock = new Mock<IResidentialManager>();
        }

        [Fact]
        public async Task Should_GetAll_ReturnsAListOfResidentials()
        {
            //Arrange

            const int totalOfItems = 10;
            var parameter = new Parameter();
            var models = new List<Residential>()
            {
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential()
            };

            var paginatedList = new PaginatedList<Residential>(models, totalOfItems);

            _managerMock.Setup(x => x.GetAllAsync(parameter))
                .ReturnsAsync(paginatedList);

            _controller = new ResidentialController(_managerMock.Object);

            //Act

            var result = await _controller.GetAll(parameter);

            //Assert

            _managerMock.Verify(x => x.GetAllAsync(parameter), Times.Once);

            var OkResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<Residential>>(OkResult.Value);

            Assert.NotNull(model);
            Assert.Equal(10, model.TotalOfItems);
            Assert.Equal(5, model.Items.Count());
        }

        [Fact]
        public async Task Should_Get_ReturnsNotFoundResult()
        {
            //Arrange

            const int residentialId = 1;

            _managerMock.Setup(x => x.ExistsAsync(residentialId))
                .ReturnsAsync(false);

            _controller = new ResidentialController(_managerMock.Object);

            //Act

            var result = await _controller.Get(residentialId);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Should_Get_ReturnsAResidentialModel()
        {
            //Arrange

            const int residentialId = 1;
            const string residentialName = "Residential Name";

            var residentialModel = new ResidentialToUpdate
            {
                Id = residentialId,
                Name = residentialName
            };

            _managerMock.Setup(x => x.ExistsAsync(residentialId))
                .ReturnsAsync(true);

            _managerMock.Setup(x => x.GetByIdAsync(residentialId))
                .ReturnsAsync(residentialModel);

            _controller = new ResidentialController(_managerMock.Object);

            //Act

            var result = await _controller.Get(residentialId);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);
            _managerMock.Verify(x => x.GetByIdAsync(residentialId), Times.Once);


            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ResidentialToUpdate>(okResult.Value);

            Assert.NotNull(model);
            Assert.Equal(residentialId, model.Id);
            Assert.Equal(residentialName, model.Name);
        }

        [Fact]
        public async Task Should_Add_ReturnsBadRequestResult()
        {
            //Arrange

            var model = new ResidentialToAdd();

            _controller = new ResidentialController(_managerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Add(model);

            //Assert

            _managerMock.Verify(x => x.AddAsync(model), Times.Never);

            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public async Task Should_Add_ReturnsNoContentResult()
        {
            //Arrange

            const string name = "Residential Name";

            var model = new ResidentialToAdd { Name = name };

            _controller = new ResidentialController(_managerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Add(model);

            //Assert

            _managerMock.Verify(x => x.AddAsync(model), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Should_Update_ReturnsNotFoundResult()
        {
            //Arrange

            const int residentialId = 1;
            const string residentialName = "Residential Name";

            var model = new ResidentialToUpdate
            {
                Id = residentialId,
                Name = residentialName
            };

            _managerMock.Setup(x => x.ExistsAsync(residentialId))
                .ReturnsAsync(false);

            _controller = new ResidentialController(_managerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Update(residentialId, model);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);
            _managerMock.Verify(x => x.UpdateAsync(model), Times.Never);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Should_Update_ReturnsBadRequestResult()
        {
            //Arrange

            const int residentialId = 1;
            var model = new ResidentialToUpdate { Id = residentialId };

            _controller = new ResidentialController(_managerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Update(residentialId, model);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(residentialId), Times.Never);
            _managerMock.Verify(x => x.UpdateAsync(model), Times.Never);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Should_Update_ReturnsNoContentResult()
        {
            //Arrange

            const int residentialId = 1;
            const string residentialName = "Residential Name";

            var model = new ResidentialToUpdate
            {
                Id = residentialId,
                Name = residentialName
            };

            _managerMock.Setup(x => x.ExistsAsync(residentialId))
                .ReturnsAsync(true);

            _controller = new ResidentialController(_managerMock.Object);
            _controller.ValidateViewModel(model);

            //Act

            var result = await _controller.Update(residentialId, model);

            //Assert

            _managerMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);
            _managerMock.Verify(x => x.UpdateAsync(model), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
