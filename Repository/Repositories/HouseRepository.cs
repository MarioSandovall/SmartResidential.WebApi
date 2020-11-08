using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly ResidentialContext _ctx;
        public HouseRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(int residentialId, int houseId)
        {
            return await _ctx.Houses
                .AnyAsync(x => x.ResidentialId == residentialId && x.Id == houseId && x.IsActive);
        }

        public async Task<bool> ExistsAsync(int residentialId, string houseName)
        {
            return await _ctx.Houses
                .AnyAsync(x => x.ResidentialId == residentialId &&
                x.IsActive && x.Name.ToUpper() == houseName.ToUpper());
        }

        public async Task<bool> ExistsAsync(int residentialId, int houseId, string houseName)
        {
            return await _ctx.Houses
                .AnyAsync(x => x.ResidentialId == residentialId &&
               x.Id != houseId && x.IsActive && x.Name.ToUpper() == houseName.ToUpper());
        }

        public IQueryable<HouseEf> AsNoTracking()
        {
            return _ctx.Houses.AsNoTracking()
                .Where(x => x.IsActive);
        }

        public async Task<HouseEf> GetByIdAsync(int houseId)
        {
            return await _ctx.Houses
                .Include(x => x.HouseUsers)
                .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == houseId && x.IsActive);
        }

        public async Task AddAsync(HouseEf entity)
        {
            _ctx.Houses.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Added;

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(HouseEf entity)
        {
            _ctx.Houses.Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int houseId)
        {
            var entity = await _ctx.Houses.FirstAsync(x => x.Id == houseId);
            entity.IsActive = false;

            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }
    }
}
