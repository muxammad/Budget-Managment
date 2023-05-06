using BudgetManagment.Domain.Commons;
using BudgetManagment.Domain.Enums;

namespace BudgetManagment.Domain.Entities
{
    public class User : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
