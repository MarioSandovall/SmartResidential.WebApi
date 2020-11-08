using AutoMapper;
using Business.Interfaces;
using Model.Models.Role;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _respository;
        public RoleManager(
            IMapper mapper,
            IRoleRepository repository)
        {
            _mapper = mapper;
            _respository = repository;
        }

        public async Task<IEnumerable<Role>> GetResidentialRolesAsync()
        {
            var entities = await _respository.GetResidentialRolesAsync();
            return _mapper.Map<IEnumerable<Role>>(entities);
        }

    }
}
