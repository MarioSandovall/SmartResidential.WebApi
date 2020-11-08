using System.Net.Mail;

namespace Model.Models.Email
{
    public class HtmlEmail
    {

        public string Title { get; set; }

        public string Content { get; set; }

        public MailMessage MailMessage { get; set; }

    }
}
