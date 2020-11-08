using Data;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ResidentialContext _ctx;
        public UserRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(int residentialId, int userId)
        {
            return await _ctx.Users
                .AnyAsync(x => x.ResidentialId == residentialId && x.Id == userId && x.IsActive);
        }

        public async Task<bool> IsAdminAsync(int residentialId, int userId)
        {
            return await _ctx.Users.Include(x => x.UserRoles)
                .AnyAsync(x => x.Id == userId && x.ResidentialId == residentialId
                && x.UserRoles.Any(r => r.RoleId == (int)RoleEnum.Admin));
        }

        public async Task<bool> ExistsEmailAsync(int residentialId, string email)
        {
            return await _ctx.Users.AnyAsync(x => x.ResidentialId == residentialId &&
                    x.Email.ToUpper() == email.Trim().ToUpper() && x.IsActive);
        }

        public async Task<bool> ExistsEmailAsync(int residentialId, int userId, string email)
        {
            return await _ctx.Users.AnyAsync(x => x.ResidentialId == residentialId &&
                    x.Id != userId && x.Email.ToUpper() == email.Trim().ToUpper() && x.IsActive);
        }

        public IQueryable<UserEf> AsNoTracking()
        {
            return _ctx.Users.AsNoTracking().Where(x => x.IsActive);
        }

        public async Task<UserEf> GetByIdAsync(int userId)
        {
            return await _ctx.Users
                .Include(x => x.UserRoles)
                .FirstAsync(x => x.Id == userId);
        }

        public async Task<UserEf> GetByIdWithResidentialAsync(int userId)
        {
            return await _ctx.Users.AsNoTracking()
                .Include(x => x.Residential).FirstAsync(x => x.Id == userId);
        }

        public async Task AddAsync(UserEf entity)
        {
            _ctx.Users.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Added;

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserEf entity)
        {
            _ctx.Users.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId)
        {
            var entity = await _ctx.Users.AsNoTracking()
                .FirstAsync(x => x.Id == userId);

            entity.IsActive = false;

            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetEmailsAsync(int residentialId)
        {
            return await _ctx.Users.AsNoTracking()
                .Where(x => x.ResidentialId == residentialId && x.IsActive)
                .Select(x => x.Email).Distinct().ToListAsync();
        }
    }
}
