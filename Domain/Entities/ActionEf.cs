using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ActionEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

    }
}
