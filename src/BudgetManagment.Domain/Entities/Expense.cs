using BudgetManagment.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagment.Domain.Entities
{
    public class Expense : Auditable
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
