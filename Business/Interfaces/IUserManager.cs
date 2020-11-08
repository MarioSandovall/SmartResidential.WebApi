using Model.Models;
using Model.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserManager
    {
        Task<bool> ExistsAsync(int residentialId, int userId);

        Task<bool> ExistsEmailAsync(int residentialId, string email);

        Task<bool> ExistsEmailAsync(int residentialId, int userId, string email);

        Task<bool> IsAdminAsync(int residentialId, int userId);

        Task<PaginatedList<User>> GetAllAsync(int residenitalId, Parameter parameter);

        Task<UserToUpdate> GetByIdAsync(int userId);

        Task UpdateAsync(UserToUpdate model);

        Task AddAsync(UserToAdd model);

        Task DeleteAsync(int userId);

        Task ResendInvitationAsync(int userId);

        Task<IEnumerable<FilteredUser>> SearchAsync(int residenitalId, string name);
    }
}
