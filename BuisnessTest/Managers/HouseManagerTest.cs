using AutoMapper;
using Business.Managers;
using Domain.Entities;
using MockQueryable.Moq;
using Model.Models;
using Model.Models.House;
using Moq;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BuisnessTest.Managers
{
    public class HouseManagerTest
    {
        private HouseManager _manager;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHouseRepository> _houseRepositoryMock;
        public HouseManagerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _houseRepositoryMock = new Mock<IHouseRepository>();
        }

        [Fact]
        public async Task Should_Call_ExistsAsyncMethodByResidentialIdAndHouseId()
        {
            //Arrange 

            const int houseId = 1;
            const int residentialId = 2;

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);

            //Act

            var result = await _manager.ExistsAsync(residentialId, houseId);

            //Assert

            _houseRepositoryMock.Verify(x => x.ExistsAsync(residentialId, houseId), Times.Once);
        }

        [Fact]
        public async Task Should_Call_ExistsAsyncMethodByResidentialIdAndHouseName()
        {
            //Arrange 

            const int residentialId = 2;
            const string houseName = "House name";

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);

            //Act

            var result = await _manager.ExistsAsync(residentialId, houseName);

            //Assert

            _houseRepositoryMock.Verify(x => x.ExistsAsync(residentialId, houseName), Times.Once);
        }

        [Fact]
        public async Task Should_Call_ExistsAsyncMethodByResidentialIdAndhouseIdAndHouseName()
        {
            //Arrange 

            const int houseId = 1;
            const int residentialId = 2;
            const string houseName = "House name";

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);

            //Act

            var result = await _manager.ExistsAsync(residentialId, houseId, houseName);

            //Assert

            _houseRepositoryMock.Verify(x => x.ExistsAsync(residentialId, houseId, houseName), Times.Once);
        }

        [Fact]
        public async Task Should_GetByIdAsync_ReturnsAHouses()
        {
            //Arrange 

            const int houseId = 1;

            var houseEntity = new HouseEf
            {
                Id = houseId
            };

            var houseModels = new HouseToUpdate
            {
                Id = houseId
            };

            var parameter = new Parameter
            {
                ItemsPerPage = 5
            };

            _houseRepositoryMock.Setup(x => x.GetByIdAsync(houseId))
                .ReturnsAsync(houseEntity);

            _mapperMock.Setup(x => x.Map<HouseToUpdate>(houseEntity))
                .Returns(houseModels);

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);


            //Act

            var model = await _manager.GetByIdAsync(houseId);

            //Assert

            _houseRepositoryMock.Verify(x => x.GetByIdAsync(houseId), Times.Once);
            _mapperMock.Verify(x => x.Map<HouseToUpdate>(houseEntity), Times.Once);

            Assert.NotNull(model);
            Assert.Equal(houseId, model.Id);
        }

        [Fact]
        public async Task Should_GetAllAsync_ReturnsAListOfHouses()
        {
            //Arrange 
            const int residentialId = 1;

            var entities = new List<HouseEf>
            {
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
                new HouseEf { ResidentialId = residentialId },
            };

            var models = new List<House>
            {
                new House(),
                new House(),
                new House(),
                new House(),
                new House()
            };

            var parameter = new Parameter
            {
                ItemsPerPage = 5
            };

            var mock = entities.AsQueryable().BuildMockDbSet();

            _houseRepositoryMock.Setup(x => x.AsNoTracking()).Returns(mock.Object);

            _mapperMock.Setup(x => x.Map<IEnumerable<House>>(It.IsAny<IEnumerable<HouseEf>>()))
                .Returns(models);

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);


            //Act

            var result = await _manager.GetAllAsync(residentialId, parameter);

            //Assert

            _houseRepositoryMock.Verify(x => x.AsNoTracking(), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<House>>(It.IsAny<IEnumerable<HouseEf>>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(10, result.TotalOfItems);
            Assert.Equal(5, result.Items.Count());
        }

        [Fact]
        public async Task Should_Call_UpdateAsync()
        {

            //Arrange

            const int houseId = 1;

            var model = new HouseToUpdate
            {
                Id = houseId
            };

            var entity = new HouseEf
            {
                Id = houseId
            };

            _houseRepositoryMock.Setup(x => x.GetByIdAsync(model.Id))
                .ReturnsAsync(entity);

            _mapperMock.Setup(x => x.Map(model, entity))
                .Returns(entity);

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);


            //Act

            await _manager.UpdateAsync(model);

            //Assert

            _houseRepositoryMock.Verify(x => x.GetByIdAsync(model.Id), Times.Once);
            _mapperMock.Verify(x => x.Map(model, entity));
            _houseRepositoryMock.Verify(x => x.UpdateAsync(entity), Times.Once);

        }

        [Fact]
        public async Task Should_Call_AddAsync()
        {
            //Arrange

            const string name = "House Name";

            var model = new HouseToAdd { Name = name };

            var entity = new HouseEf { Name = name };

            _mapperMock.Setup(x => x.Map<HouseEf>(It.IsAny<HouseToAdd>()))
                .Returns(entity);

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);

            //Act

            await _manager.AddAsync(model);

            //Assert

            _mapperMock.Verify(x => x.Map<HouseEf>(It.IsAny<HouseToAdd>()));
            _houseRepositoryMock.Verify(x => x.AddAsync(entity), Times.Once);
        }

        [Fact]
        public async Task Should_Call_DeleteAsync()
        {
            //Arrange 

            const int houseId = 1;

            _manager = new HouseManager(_mapperMock.Object, _houseRepositoryMock.Object);

            //Act

            await _manager.DeleteAsync(houseId);

            //Assert

            _houseRepositoryMock.Verify(x => x.DeleteAsync(houseId), Times.Once);
        }
    }
}
