using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AdvertisementEf : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(300)]
        public string ImagePath { get; set; }

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

        public bool IsActive { get; set; }

    }
}
