using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentCategoryEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int ResidentialId { get; set; }
        public ResidentialEf Residential { get; set; }

        public bool IsActive { get; set; }

    }
}



