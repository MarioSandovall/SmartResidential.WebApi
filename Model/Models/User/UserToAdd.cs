using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models.User
{
    public class UserToAdd
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string CellPhone { get; set; }

        [StringLength(20)]
        public string LandPhone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public int ResidentialId { get; set; }

        [Required]
        public IEnumerable<int> Roles { get; set; }

        public bool CanSendInvitationEmail { get; set; }

    }
}
