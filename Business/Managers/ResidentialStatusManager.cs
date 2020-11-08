using AutoMapper;
using Business.Interfaces;
using Model.Models.Residential;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ResidentialStatusManager : IResidentialStatusManager
    {
        private readonly IMapper _mapper;
        private readonly IResidentialStatusRepository _repository;
        public ResidentialStatusManager(
            IMapper mapper,
            IResidentialStatusRepository residentialStatusRepository)
        {
            _mapper = mapper;
            _repository = residentialStatusRepository;

        }

        public async Task<IEnumerable<ResidentialStatus>> GetAllAsync()
        {
            var statusEntities = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<ResidentialStatus>>(statusEntities);
        }

    }
}
