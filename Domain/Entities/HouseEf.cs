using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class HouseEf : IEntity
    {
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

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<HouseUserEf> HouseUsers { get; set; }
    }
}
