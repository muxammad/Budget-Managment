using AutoMapper;
using BudgetManagment.DataAccess.Interfaces;
using BudgetManagment.Domain.Configurations;
using BudgetManagment.Domain.Entities;
using BudgetManagment.Service.Commons.Exceptions;
using BudgetManagment.Service.Commons.Extensions;
using BudgetManagment.Service.DTOs.Expenses;
using BudgetManagment.Service.Interfaces;
namespace BudgetManagment.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IGenericRepository<Expense> repository;

        public ExpenseService(IUserService userService,
            IGenericRepository<Expense> repository, IMapper mapper)
        {
            this.userService = userService;
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ExpenseForResultDto> AddAsync(ExpenseCreationDto dto)
        {
            var user = await this.userService.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new CustomException(404, "Couldn't find income for given id");

            var expense = await this.repository.SelectAsync(i => i.UserId == dto.UserId);
            if (expense is not null)
            {
                var updated = await this.repository.UpdateAsync(expense);
                updated.UpdatedAt = DateTime.UtcNow;
                await this.repository.SaveAsync();
            }

            var mapped = this.mapper.Map<Expense>(dto);
            var inserted = await this.repository.InsertAsync(mapped);
            await this.repository.SaveAsync();

            return this.mapper.Map<ExpenseForResultDto>(inserted);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expense = await this.repository.SelectAsync(i => i.Id == id);
            if (expense is null)
                throw new CustomException(404, "Couldn't find expense for given id");

            var res = await this.repository.DeleteAsync(expense);
            await this.repository.SaveAsync();

            return res;
        }

        public async Task<IEnumerable<ExpenseForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var expenses = this.repository.SelectAll().ToPagedList(@params);
            if (expenses is null)
                throw new CustomException(404, "Couldn't find incomes for given id");

            return this.mapper.Map<IEnumerable<ExpenseForResultDto>>(expenses);
        }

        public async Task<ExpenseForResultDto> GetByIdAsync(int id)
        {
            var expense = await this.repository.SelectAsync(i => i.Id == id);
            if (expense is null)
                throw new CustomException(404, "Couldn't find income for given id");

            return this.mapper.Map<ExpenseForResultDto>(expense);
        }

        public async Task<ExpenseForResultDto> UpdateAsync(int id, ExpenseCreationDto dto)
        {
            var expense = await this.repository.SelectAsync(i => i.Id == id);
            if (expense == null)
                throw new CustomException(404, "Couldn't find user for given id");

            var modified = this.mapper.Map(dto, expense);

            var updated = await this.repository.UpdateAsync(modified);
            updated.UpdatedAt = DateTime.UtcNow;
            await this.repository.SaveAsync();
            return this.mapper.Map<ExpenseForResultDto>(updated);
        }
    }
}
