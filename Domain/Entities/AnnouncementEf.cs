using Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AnnouncementEf : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public int CreatorId { get; set; }
        public UserEf Creator { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }
    }
}
