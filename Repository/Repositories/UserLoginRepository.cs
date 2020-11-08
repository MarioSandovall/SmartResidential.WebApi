using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly ResidentialContext _ctx;
        public UserLoginRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(string email, string password)
        {
            return await _ctx.Users.AnyAsync(x => x.IsActive && x.Email.ToUpper() == email.ToUpper() && x.Password == password);
        }

        public async Task<UserEf> GetUserAsync(string email, string password)
        {
            return await _ctx.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstAsync(x => x.IsActive && x.Email.ToUpper() == email.ToUpper() && x.Password == password);
        }

        public async Task<UserEf> GetUserAsync(int id)
        {
            return await _ctx.Users
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .Include(x => x.Residential).FirstAsync(x => x.Id == id);
        }

    }
}
