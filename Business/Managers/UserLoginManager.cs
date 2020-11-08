using AutoMapper;
using Business.Interfaces;
using Model.Models.Login;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class UserLoginManager : IUserLoginManager
    {
        private readonly IMapper _mapper;
        private readonly IUserLoginRepository _repository;
        public UserLoginManager(
            IMapper mapper,
            IUserLoginRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(string email, string password)
        {
            return await _repository.ExistsAsync(email, password);
        }

        public async Task<UserLogin> GetUserAsync(string email, string password)
        {
            var entity = await _repository.GetUserAsync(email, password);

            return _mapper.Map<UserLogin>(entity);
        }

        public async Task<UserLogin> GetUserAsync(int id)
        {
            var entity = await _repository.GetUserAsync(id);

            return _mapper.Map<UserLogin>(entity);
        }
    }
}
