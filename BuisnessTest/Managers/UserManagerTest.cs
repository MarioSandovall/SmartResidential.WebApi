using AutoMapper;
using Moq;

namespace BuisnessTest.Managers
{
    public class UserManagerTest
    {
        private readonly Mock<IMapper> _mapperMock;
        public UserManagerTest()
        {
            _mapperMock = new Mock<IMapper>();
        }
    }
}
