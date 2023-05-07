using BudgetManagment.Domain.Configurations;
using BudgetManagment.Service.DTOs.User;

namespace BudgetManagment.Service.Interfaces
{
    public interface IUserService
    {
        public Task<UserForResultDto> AddAsync(UserCreationDto dto);
        public Task<UserForResultDto> UpdateAsync(int id, UserCreationDto dto);
        public Task<bool> DeleteAsync(int id);
        public Task<UserForResultDto> GetByIdAsync(int id);
        public Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
