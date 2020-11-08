using Domain.Entities;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserLoginRepository
    {

        Task<bool> ExistsAsync(string email, string password);

        Task<UserEf> GetUserAsync(string email, string password);

        Task<UserEf> GetUserAsync(int id);

    }
}
