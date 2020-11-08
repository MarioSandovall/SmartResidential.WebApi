using Model.Settings;
using System.Net;
using System.Net.Mail;

namespace Service.Extensions
{
    internal static class SmtpExtension
    {
        public static void Setup(this SmtpClient client, EmailConfiguration settings)
        {
            client.EnableSsl = false;
            client.Port = settings.Port;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(settings.Sender, settings.Password);
        }
    }
}
