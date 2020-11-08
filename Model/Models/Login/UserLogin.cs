using System.Collections.Generic;

namespace Model.Models.Login
{
    public class UserLogin
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string CellPhone { get; set; }

        public string LandPhone { get; set; }

        public string Email { get; set; }

        public int? ResidentialId { get; set; }

        public string Residential { get; set; }

        public IEnumerable<int> RoleIds { get; set; }

        public IEnumerable<string> Roles { get; set; }


    }
}
