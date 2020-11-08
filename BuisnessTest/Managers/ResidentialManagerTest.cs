using AutoMapper;
using Business.Managers;
using Domain.Entities;
using MockQueryable.Moq;
using Model.Models;
using Model.Models.Residential;
using Moq;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BuisnessTest.Managers
{
    public class ResidentialManagerTest
    {
        private ResidentialManager _manager;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IResidentialRepository> _repositoryMock;
        public ResidentialManagerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IResidentialRepository>();
        }

        [Fact]
        public async Task Should_Call_ExistsAsync()
        {
            //Arrange 

            const int residentialId = 1;

            _manager = new ResidentialManager(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await _manager.ExistsAsync(residentialId);

            //Assert

            _repositoryMock.Verify(x => x.ExistsAsync(residentialId), Times.Once);
        }

        [Fact]
        public async Task Should_GetAllAsync_ReturnsAListOfResidentials()
        {
            //Arrange 

            var entities = new List<Residential>
            {
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential(),
                new Residential()
            };

            var parameter = new Parameter
            {
                ItemsPerPage = 5
            };

            var mock = entities.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(x => x.AsNoTracking()).Returns(mock.Object);

            _manager = new ResidentialManager(_mapperMock.Object, _repositoryMock.Object);


            //Act

            var result = await _manager.GetAllAsync(parameter);

            //Assert

            _repositoryMock.Verify(x => x.AsNoTracking(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(10, result.TotalOfItems);
            Assert.Equal(5, result.Items.Count());
        }

        [Fact]
        public async Task Should_GetByIdAsync_ReturnsAResidential()
        {
            //Arrange 

            const int residentialId = 1;

            var houseEntity = new ResidentialEf
            {
                Id = residentialId
            };

            var houseModels = new ResidentialToUpdate
            {
                Id = residentialId
            };

            var parameter = new Parameter
            {
                ItemsPerPage = 5
            };

            _repositoryMock.Setup(x => x.GetByIdAsync(residentialId))
                .ReturnsAsync(houseEntity);

            _mapperMock.Setup(x => x.Map<ResidentialToUpdate>(houseEntity))
                .Returns(houseModels);

            _manager = new ResidentialManager(_mapperMock.Object, _repositoryMock.Object);


            //Act

            var model = await _manager.GetByIdAsync(residentialId);

            //Assert

            _repositoryMock.Verify(x => x.GetByIdAsync(residentialId), Times.Once);
            _mapperMock.Verify(x => x.Map<ResidentialToUpdate>(houseEntity), Times.Once);

            Assert.NotNull(model);
            Assert.Equal(residentialId, model.Id);
        }

        [Fact]
        public async Task Should_Call_UpdateAsync()
        {

            //Arrange

            const int residentialId = 1;

            var model = new ResidentialToUpdate
            {
                Id = residentialId
            };

            var entity = new ResidentialEf
            {
                Id = residentialId
            };

            _repositoryMock.Setup(x => x.GetByIdAsync(model.Id))
                .ReturnsAsync(entity);

            _mapperMock.Setup(x => x.Map(model, entity))
                .Returns(entity);

            _manager = new ResidentialManager(_mapperMock.Object, _repositoryMock.Object);


            //Act

            await _manager.UpdateAsync(model);

            //Assert

            _repositoryMock.Verify(x => x.GetByIdAsync(model.Id), Times.Once);
            _mapperMock.Verify(x => x.Map(model, entity));
            _repositoryMock.Verify(x => x.UpdateAsync(entity), Times.Once);

        }

        [Fact]
        public async Task Should_Call_AddAsync()
        {
            //Arrange

            const string name = "Residential Name";

            var model = new ResidentialToAdd { Name = name };

            var entity = new ResidentialEf { Name = name };

            _mapperMock.Setup(x => x.Map<ResidentialEf>(It.IsAny<ResidentialToAdd>()))
                .Returns(entity);

            _manager = new ResidentialManager(_mapperMock.Object, _repositoryMock.Object);

            //Act

            await _manager.AddAsync(model);

            //Assert

            _mapperMock.Verify(x => x.Map<ResidentialEf>(It.IsAny<ResidentialToAdd>()));
            _repositoryMock.Verify(x => x.AddAsync(entity), Times.Once);
        }

    }
}
