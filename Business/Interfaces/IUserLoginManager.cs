using Model.Models.Login;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserLoginManager
    {
        Task<bool> ExistsAsync(string email, string password);

        Task<UserLogin> GetUserAsync(string email, string password);

        Task<UserLogin> GetUserAsync(int id);
    }
}
