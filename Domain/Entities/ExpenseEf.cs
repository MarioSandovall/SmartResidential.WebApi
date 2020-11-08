using Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ExpenseEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int IsActive { get; set; }

        public int ExpenseCategoryId { get; set; }
        public ExpenseCategoryEf ExpenseCategory { get; set; }

        [Required]
        public int CreatorId { get; set; }
        public UserEf Creator { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

    }
}
