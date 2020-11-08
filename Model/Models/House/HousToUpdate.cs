using Model.Models.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models.House
{
    public class HouseToUpdate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [StringLength(10)]
        public string InteriorNumber { get; set; }

        [StringLength(10)]
        public string OutdoorNumber { get; set; }

        [Required]
        public int ResidentialId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<FilteredUser> Users { get; set; }
    }
}
