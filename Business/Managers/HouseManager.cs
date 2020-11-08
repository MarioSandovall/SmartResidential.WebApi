using AutoMapper;
using Business.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Models.House;
using Repository.Extensions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class HouseManager : IHouseManager
    {
        private readonly IMapper _mapper;
        private readonly IHouseRepository _repository;
        public HouseManager(
            IMapper mapper,
            IHouseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(int residentialId, int houseId)
        {
            return await _repository.ExistsAsync(residentialId, houseId);
        }

        public async Task<bool> ExistsAsync(int residentialId, string houseName)
        {
            return await _repository.ExistsAsync(residentialId, houseName);
        }

        public async Task<bool> ExistsAsync(int residentialId, int houseId, string houseName)
        {
            return await _repository.ExistsAsync(residentialId, houseId, houseName);
        }

        public async Task<PaginatedList<House>> GetAllAsync(int residentialId, Parameter parameter)
        {
            var query = _repository.AsNoTracking()
                .Include(x => x.HouseUsers)
                .ThenInclude(x => x.User)
                .Where(x => x.ResidentialId == residentialId)
                .Contains(parameter.Filter).ApplySort(parameter.SortBy, parameter.IsSortDesc);

            var totalOfItems = await query.CountAsync();
            var entities = await query.Take(parameter.Page, parameter.ItemsPerPage).ToListAsync();

            var models = _mapper.Map<IEnumerable<House>>(entities);

            return new PaginatedList<House>(models, totalOfItems);
        }

        public async Task<HouseToUpdate> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return _mapper.Map<HouseToUpdate>(entity);
        }

        public async Task UpdateAsync(HouseToUpdate model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);

            _mapper.Map(model, entity);

            await _repository.UpdateAsync(entity);
        }

        public async Task AddAsync(HouseToAdd model)
        {
            var entity = _mapper.Map<HouseEf>(model);

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

    }
}
