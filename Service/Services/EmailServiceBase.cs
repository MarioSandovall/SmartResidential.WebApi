using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Model.Models.Email;
using Model.Settings;
using Repository.Interfaces;
using Service.Extensions;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailServiceBase
    {
        protected IMapper Mapper;
        protected string ContentRootPath;
        protected EmailConfiguration EmailConfiguration;
        protected IResidentialRepository ResidentialRepository;

        protected EmailServiceBase(
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            IOptions<EmailConfiguration> emailOptions,
            IResidentialRepository residentialRepository)
        {
            Mapper = mapper;
            EmailConfiguration = emailOptions.Value;
            ResidentialRepository = residentialRepository;
            ContentRootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<HtmlEmail> CreateHtmlEmailAsync(int residentialId, string subjet)
        {
            if (await ResidentialRepository.ExistsAsync(residentialId))
            {
                var residential = await ResidentialRepository.GetByIdAsync(residentialId);

                subjet = $"{residential.Name} - {subjet}";
            }

            var mailMessage = new MailMessage
            {
                Subject = subjet,
                IsBodyHtml = true,
                From = new MailAddress(EmailConfiguration.Sender),
            };

            return new HtmlEmail
            {
                MailMessage = mailMessage
            };
        }

        public void Send(HtmlEmail htmlEmail, params string[] emails)
        {
            if (EmailConfiguration.IsEnabled)
            {
                Task.Run(() =>
                {
                    using (var client = new SmtpClient(EmailConfiguration.SmtpClient))
                    {
                        client.Setup(EmailConfiguration);

                        var mailMessage = htmlEmail.ToBuildMailMessage(ContentRootPath);

                        mailMessage.AddEmails(emails);

                        if (mailMessage.To.Any())
                        {
                            client.Send(mailMessage);
                        }
                    };
                });
            }
        }
    }
}
