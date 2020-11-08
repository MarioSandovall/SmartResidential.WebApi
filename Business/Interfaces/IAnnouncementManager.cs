using Model.Models.Announcement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAnnouncementManager
    {
        Task<bool> ExistsAsync(int residentialId, int announcementId);

        Task<IEnumerable<Announcement>> GetAllAsync(int residentialId);

        Task<AnnouncementToUpdate> GetByIdAsync(int id);

        Task UpdateAsync(AnnouncementToUpdate model);

        Task AddAsync(AnnouncementToAdd model);

        Task DeleteAsync(int id);

        Task ResendEmailAsync(int announcementId);
    }
}
