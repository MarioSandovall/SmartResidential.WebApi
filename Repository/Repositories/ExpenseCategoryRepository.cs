using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly ResidentialContext _ctx;
        public ExpenseCategoryRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(int residentialId, int categoryId)
        {
            return await _ctx.ExpenseCategories.
                AnyAsync(x => x.ResidentialId == residentialId && x.Id == categoryId && x.IsActive);
        }

        public async Task<bool> ExistsAsync(int residentialId, string categoryName)
        {
            return await _ctx.ExpenseCategories
                .AnyAsync(x => x.ResidentialId == residentialId &&
                x.Name.Trim().ToUpper() == categoryName.Trim().ToUpper() && x.IsActive);
        }

        public async Task<bool> ExistsAsync(int residentialId, int categoryId, string categoryName)
        {
            return await _ctx.ExpenseCategories
                .AnyAsync(x => x.ResidentialId == residentialId && x.Id != categoryId &&
                    x.Name.Trim().ToUpper() == categoryName.Trim().ToUpper() && x.IsActive);
        }

        public IQueryable<ExpenseCategoryEf> AsNoTracking()
        {
            return _ctx.ExpenseCategories
                .AsNoTracking().Where(x => x.IsActive);
        }

        public async Task<ExpenseCategoryEf> GetByIdAsync(int categoryId)
        {
            return await _ctx.ExpenseCategories
                .FirstAsync(x => x.Id == categoryId);
        }

        public async Task AddAsync(ExpenseCategoryEf entity)
        {
            _ctx.Entry(entity).State = EntityState.Added;

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExpenseCategoryEf entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int categoryId)
        {
            var entity = await _ctx.ExpenseCategories
                .AsNoTracking().FirstAsync(x => x.Id == categoryId);

            entity.IsActive = false;

            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }
    }
}
