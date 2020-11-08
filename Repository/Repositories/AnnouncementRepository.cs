using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ResidentialContext _ctx;
        public AnnouncementRepository(ResidentialContext ctx) => _ctx = ctx;

        public async Task<bool> ExistsAsync(int residentialId, int announcementId)
        {
            return await _ctx.Announcements.
                AnyAsync(x => x.ResidentialId == residentialId && x.Id == announcementId);
        }

        public IQueryable<AnnouncementEf> AsNoTracking()
        {
            return _ctx.Announcements.AsNoTracking();
        }

        public async Task<AnnouncementEf> GetByIdAsync(int announcementId)
        {
            return await _ctx.Announcements
                .FirstAsync(x => x.Id == announcementId);
        }

        public async Task AddAsync(AnnouncementEf entity)
        {
            _ctx.Entry(entity).State = EntityState.Added;

            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnnouncementEf entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int announcementId)
        {
            var entity = await _ctx.Announcements
                .AsNoTracking().FirstAsync(x => x.Id == announcementId);

            _ctx.Entry(entity).State = EntityState.Deleted;

            await _ctx.SaveChangesAsync();
        }

    }
}
