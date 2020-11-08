using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Model.Models.Email;
using Model.Settings;
using Repository.Interfaces;
using Service.Extensions;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailService : EmailServiceBase, IEmailService
    {
        public EmailService(
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IOptions<EmailConfiguration> emailOptions,
            IResidentialRepository residentialRepository)
            : base(mapper, hostingEnvironment, emailOptions, residentialRepository)
        {

        }

        public async Task SendInvitationAsync(UserEf user)
        {
            var invitation = Mapper.Map<InvitationEmail>(user);
            invitation.Url = EmailConfiguration.Url;

            var residentialId = user.ResidentialId.Value;
            const string subjet = "Bienvenido a Smart Residential";

            var htmlEmail = await CreateHtmlEmailAsync(residentialId, subjet);

            htmlEmail.Title = invitation.Residential;
            htmlEmail.Content = invitation.ToHtmlContent(ContentRootPath);

            Send(htmlEmail, user.Email);

        }

        public async Task SendAnnouncementAsync(AnnouncementEf announcement, IEnumerable<string> emails)
        {
            var subjet = announcement.Title;
            var htmlEmail = await CreateHtmlEmailAsync(announcement.ResidentialId, subjet);

            htmlEmail.Title = announcement.Title;
            htmlEmail.Content = announcement.Description.ToHtml();

            Send(htmlEmail, emails.ToArray());
        }

    }
}
