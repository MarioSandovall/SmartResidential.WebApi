using Model.Models.Email;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Service.Extensions
{
    internal static class EmailExtension
    {

        public static void AddEmails(this MailMessage mailMessage, string[] emails)
        {
            var validEmails = emails.Where(email => IsEmailValid(email)).Distinct().ToList();

            foreach (var email in validEmails)
            {
                mailMessage.To.Add(email);
            }
        }

        public static MailMessage ToBuildMailMessage(this HtmlEmail htmlEmail, string contentRootPath)
        {
            var htmlString = htmlEmail.GetHtmlBase(contentRootPath);

            AddAlternateView(htmlEmail.MailMessage, htmlString);

            return htmlEmail.MailMessage;
        }

        private static void AddAlternateView(MailMessage mailMessage, string htmlString)
        {
            var htmlView = AlternateView.CreateAlternateViewFromString(htmlString, null, "text/html");

            mailMessage.AlternateViews.Add(htmlView);
        }

        private static bool IsEmailValid(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.Match(email).Success;
        }

    }
}
