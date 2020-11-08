using System.ComponentModel.DataAnnotations;

namespace Model.Models.ExpenseCategory
{
    public class ExpenseCategoryToUpdate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
