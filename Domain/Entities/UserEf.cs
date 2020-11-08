using Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEf : IEntity
    {
        public int Id { get; set; }

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
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(300)]
        public string ProfileImagePath { get; set; }

        public int? ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

        public ICollection<UserRoleEf> UserRoles { get; set; }

        public ICollection<HouseUserEf> HouseUsers { get; set; }
    }
}
