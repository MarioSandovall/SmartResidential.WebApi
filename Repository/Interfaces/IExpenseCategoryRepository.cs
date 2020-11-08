using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        Task<bool> ExistsAsync(int residentialId, int categoryId);

        Task<bool> ExistsAsync(int residentialId, string categoryName);

        Task<bool> ExistsAsync(int residentialId, int categoryId, string categoryName);

        IQueryable<ExpenseCategoryEf> AsNoTracking();

        Task<ExpenseCategoryEf> GetByIdAsync(int categoryId);

        Task AddAsync(ExpenseCategoryEf entity);

        Task UpdateAsync(ExpenseCategoryEf entity);

        Task DeleteAsync(int categoryId);
    }
}
