using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ExpenseCategoryEf : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
