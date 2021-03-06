﻿using System.ComponentModel.DataAnnotations;

namespace Model.Models.Residential
{
    public class ResidentialToUpdate
    {
        [Required]
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

        [Required]
        public int StatusId { get; set; }

    }
}
