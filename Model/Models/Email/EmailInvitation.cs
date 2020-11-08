namespace Model.Models.Email
{
    public class InvitationEmail : EmailBase
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Residential { get; set; }

    }
}
