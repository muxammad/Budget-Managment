using BudgetManagment.Domain.Configurations;
using BudgetManagment.Service.DTOs.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagment.Service.Interfaces
{
    public interface IExpenseService
    {
        public Task<bool> DeleteAsync(int id);
        public Task<ExpenseForResultDto> GetByIdAsync(int id);
        public Task<ExpenseForResultDto> AddAsync(ExpenseCreationDto dto);
        public Task<ExpenseForResultDto> UpdateAsync(int id, ExpenseCreationDto dto);
        public Task<IEnumerable<ExpenseForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
