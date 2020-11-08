using AutoMapper;
using Business.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Models.Residential;
using Repository.Extensions;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ResidentialManager : IResidentialManager
    {
        private readonly IMapper _mapper;
        private readonly IResidentialRepository _repository;
        public ResidentialManager(
            IMapper mapper,
            IResidentialRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(int residentialId)
        {
            return await _repository.ExistsAsync(residentialId);
        }

        public async Task<PaginatedList<Residential>> GetAllAsync(Parameter parameter)
        {
            var query = _repository.AsNoTracking()
                .Contains(parameter.Filter).ApplySort(parameter.SortBy, parameter.IsSortDesc);

            var totalOfItems = await query.CountAsync();
            var items = await query.Take(parameter.Page, parameter.ItemsPerPage).ToListAsync();

            return new PaginatedList<Residential>(items, totalOfItems);
        }

        public async Task<ResidentialToUpdate> GetByIdAsync(int residentialId)
        {
            var entity = await _repository.GetByIdAsync(residentialId);

            return _mapper.Map<ResidentialToUpdate>(entity);
        }

        public async Task UpdateAsync(ResidentialToUpdate model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);

            _mapper.Map(model, entity);

            await _repository.UpdateAsync(entity);
        }

        public async Task AddAsync(ResidentialToAdd model)
        {
            var entity = _mapper.Map<ResidentialEf>(model);
  
            await _repository.AddAsync(entity);
        }
    }
}
