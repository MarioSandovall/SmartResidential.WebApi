using AutoMapper;
using Business.Managers;
using Domain.Entities;
using Model.Models.Residential;
using Moq;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BuisnessTest.Managers
{
    public class ResidentialStatusManagerTest
    {
        private ResidentialStatusManager _manager;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IResidentialStatusRepository> _repositoryMock;
        public ResidentialStatusManagerTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IResidentialStatusRepository>();
        }


        [Fact]
        public async Task Should_GetAllAsync_ReturnsAListOfResidentialStatus()
        {
            //Arrange 

            var statusEntities = new List<ResidentialStatusEf>
            {
                new ResidentialStatusEf(),
                new ResidentialStatusEf(),
                new ResidentialStatusEf()
            };

            var statusModels = new List<ResidentialStatus>
            {
                new ResidentialStatus(),
                new ResidentialStatus(),
                new ResidentialStatus()
            };

            _repositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(statusEntities);

            _mapperMock.Setup(x => x.Map<IEnumerable<ResidentialStatus>>(statusEntities))
                .Returns(statusModels);

            _manager = new ResidentialStatusManager(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var models = await _manager.GetAllAsync();

            //Assert

            _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<ResidentialStatus>>(statusEntities), Times.Once);

            Assert.NotNull(models);
            Assert.Equal(3, models.Count());
        }
    }
}
