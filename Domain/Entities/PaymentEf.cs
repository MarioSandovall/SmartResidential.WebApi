using Domain.Interfaces;
using Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }

        public PaymentStatusEnum PaymentStatus { get; set; }

        public int HouseId { get; set; }
        public HouseEf House { get; set; }

        public int PaymentCategoryId { get; set; }
        public PaymentCategoryEf PaymentCategory { get; set; }

    }
}
