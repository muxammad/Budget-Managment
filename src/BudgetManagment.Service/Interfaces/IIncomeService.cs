using BudgetManagment.Domain.Configurations;
using BudgetManagment.Service.DTOs.Incomes;

namespace BudgetManagment.Service.Interfaces
{
    public interface IIncomeService
    {
        public Task<bool> DeleteAsync(int id);
        public Task<IncomeForResultDto> GetByIdAsync(int id);
        public Task<IncomeForResultDto> AddAsync(IncomeCreationDto dto);
        public Task<IncomeForResultDto> UpdateAsync(int id, IncomeCreationDto dto);
        public Task<IEnumerable<IncomeForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
