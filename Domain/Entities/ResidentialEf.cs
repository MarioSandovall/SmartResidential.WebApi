using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ResidentialEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Cellphone { get; set; }

        [StringLength(20)]
        public string LandPhone { get; set; }

        [StringLength(300)]
        public string LogoPath { get; set; }

        public int ResidentialStatusId { get; set; }

        public ResidentialStatusEf ResidentialStatus { get; set; }
    }
}
