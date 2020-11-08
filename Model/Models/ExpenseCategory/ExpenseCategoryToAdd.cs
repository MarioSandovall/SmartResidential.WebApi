using System.ComponentModel.DataAnnotations;

namespace Model.Models.ExpenseCategory
{
    public class ExpenseCategoryToAdd
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int ResidentialId { get; set; }
    }
}
