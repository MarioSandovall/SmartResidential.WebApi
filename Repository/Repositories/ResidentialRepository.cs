using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models.Residential;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ResidentialRepository : IResidentialRepository
    {
        private readonly ResidentialContext _ctx;
        public ResidentialRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(int residentialId)
        {
            return await _ctx.Residentials.AnyAsync(x => x.Id == residentialId);
        }

        public async Task<ResidentialEf> GetByIdAsync(int residentialId)
        {
            return await _ctx.Residentials
                .AsNoTracking().FirstAsync(x => x.Id == residentialId);
        }

        public IQueryable<Residential> AsNoTracking()
        {
            return _ctx.Residentials.AsNoTracking()
                .Include(x => x.ResidentialStatus)
                .Select(x => new Residential
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Cellphone = x.Cellphone,
                    LandPhone = x.LandPhone,
                    LogoPath = x.LogoPath,
                    Status = x.ResidentialStatus.Name
                });
        }

        public async Task AddAsync(ResidentialEf entity)
        {
            _ctx.Entry(entity).State = EntityState.Added;
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(ResidentialEf entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

    }
}
