using Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AreaEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(300)]
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

        [Required]
        public int CreatorId { get; set; }
        public UserEf Creator { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

    }
}
