using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmailService
    {
        Task SendInvitationAsync(UserEf user);

        Task SendAnnouncementAsync(AnnouncementEf announcement, IEnumerable<string> emails);
    }
}
