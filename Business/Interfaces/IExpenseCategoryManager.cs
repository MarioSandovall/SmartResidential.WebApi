using Model.Models;
using Model.Models.ExpenseCategory;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IExpenseCategoryManager
    {
        Task<bool> ExistsAsync(int residentialId, int categoryId);

        Task<bool> ExistsAsync(int residentialId, string categoryName);

        Task<bool> ExistsAsync(int residentialId, int categoryId, string categoryName);

        Task<PaginatedList<ExpenseCategory>> GetAllAsync(int residentialId, Parameter parameter);

        Task<ExpenseCategory> GetByIdAsync(int categoryId);

        Task UpdateAsync(ExpenseCategoryToUpdate model);

        Task AddAsync(ExpenseCategoryToAdd model);

        Task DeleteAsync(int categoryId);
    }
}
