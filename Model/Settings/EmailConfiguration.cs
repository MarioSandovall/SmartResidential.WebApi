namespace Model.Settings
{
    public class EmailConfiguration
    {
        public int Port { get; set; }

        public string Url { get; set; }

        public string Sender { get; set; }

        public string Password { get; set; }

        public string SmtpClient { get; set; }

        public bool IsEnabled { get; set; }

    }
}
