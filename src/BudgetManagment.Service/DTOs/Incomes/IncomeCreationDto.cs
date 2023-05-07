using System.ComponentModel.DataAnnotations;

namespace BudgetManagment.Service.DTOs.Incomes
{
    public class IncomeCreationDto
    {
        [Required(ErrorMessage = "User id is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;
    }
}
