using Model.Models.Email;
using Service.Utils;
using System.IO;
using System.Text;

namespace Service.Extensions
{
    internal static class EmailContentExtension
    {
        public static string ToHtmlContent(this InvitationEmail email, string contentRootPath)
        {
            var builder = new StringBuilder();
            var templateFile = Path.Combine(contentRootPath, EmailPaths.Welcome);

            builder.Append(File.ReadAllText(templateFile));

            builder.Replace(EmailFields.Url, email.Url);
            builder.Replace(EmailFields.Email, email.Email);
            builder.Replace(EmailFields.UserName, email.UserName);
            builder.Replace(EmailFields.Password, email.Password);

            return builder.ToString();
        }


        public static string GetHtmlBase(this HtmlEmail htmlEmail, string contentRootPath)
        {
            var builder = new StringBuilder();
            var templateBase = Path.Combine(contentRootPath, EmailPaths.Base);

            builder.Append(File.ReadAllText(templateBase));

            builder.Replace(EmailFields.Title, htmlEmail.Title);
            builder.Replace(EmailFields.Content, htmlEmail.Content);

            return builder.ToString();
        }




    }
}
