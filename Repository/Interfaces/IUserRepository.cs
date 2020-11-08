using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(int residentialId, int userId);

        Task<bool> ExistsEmailAsync(int residentialId, string email);

        Task<bool> ExistsEmailAsync(int residentialId, int userId, string email);

        Task<bool> IsAdminAsync(int userId, int residentialId);

        IQueryable<UserEf> AsNoTracking();

        Task<UserEf> GetByIdAsync(int userId);

        Task<UserEf> GetByIdWithResidentialAsync(int userId);

        Task AddAsync(UserEf entity);

        Task UpdateAsync(UserEf entity);

        Task DeleteAsync(int userId);

        Task<IEnumerable<string>> GetEmailsAsync(int residentialId);
    }
}
