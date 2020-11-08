using AutoMapper;
using Business.Interfaces;
using Business.Utils;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Models.ExpenseCategory;
using Repository.Extensions;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ExpenseCategoryManager : IExpenseCategoryManager
    {
        private readonly IMapper _mapper;
        private readonly IExpenseCategoryRepository _repository;
        public ExpenseCategoryManager(
            IMapper mapper,
            IExpenseCategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(int residentialId, int categoryId)
        {
            return await _repository.ExistsAsync(residentialId, categoryId);
        }

        public async Task<bool> ExistsAsync(int residentialId, int categoryId, string categoryName)
        {
            return await _repository.ExistsAsync(residentialId, categoryId, categoryName);
        }

        public async Task<bool> ExistsAsync(int residentialId, string categoryName)
        {
            return await _repository.ExistsAsync(residentialId, categoryName);
        }

        public async Task<PaginatedList<ExpenseCategory>> GetAllAsync(int residentialId, Parameter parameter)
        {

            var columnsToFilter = ColumnFilter.GetNames<ExpenseCategoryEf>(x => x.Name, x => x.Description);

            var query = _repository.AsNoTracking()
                .Where(x => x.ResidentialId == residentialId)
                .Contains(parameter.Filter, columnsToFilter).ApplySort(parameter.SortBy, parameter.IsSortDesc);

            var totalOfItems = await query.CountAsync();
            var entities = await query.Take(parameter.Page, parameter.ItemsPerPage).ToListAsync();

            var models = _mapper.Map<IEnumerable<ExpenseCategory>>(entities);
            return new PaginatedList<ExpenseCategory>(models, totalOfItems);
        }

        public async Task<ExpenseCategory> GetByIdAsync(int categoryId)
        {
            var entity = await _repository.GetByIdAsync(categoryId);

            return _mapper.Map<ExpenseCategory>(entity);
        }

        public async Task UpdateAsync(ExpenseCategoryToUpdate model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);

            _mapper.Map(model, entity);

            await _repository.UpdateAsync(entity);
        }

        public async Task AddAsync(ExpenseCategoryToAdd model)
        {
            var entity = _mapper.Map<ExpenseCategoryEf>(model);

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int categoryId)
        {
            await _repository.DeleteAsync(categoryId);
        }

    }
}
