using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<bool> ExistsAsync(int residentialId, int announcementId);

        IQueryable<AnnouncementEf> AsNoTracking();

        Task<AnnouncementEf> GetByIdAsync(int announcementId);

        Task AddAsync(AnnouncementEf entity);

        Task UpdateAsync(AnnouncementEf entity);

        Task DeleteAsync(int announcementId);
    }
}
