using BudgetManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagment.DataAccess.Context
{
    public class BmDbContext : DbContext
    {
        public BmDbContext(DbContextOptions<BmDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
