using AutoMapper;
using BudgetManagment.DataAccess.Interfaces;
using BudgetManagment.Domain.Configurations;
using BudgetManagment.Domain.Entities;
using BudgetManagment.Service.Commons.Exceptions;
using BudgetManagment.Service.Commons.Extensions;
using BudgetManagment.Service.DTOs.Incomes;
using BudgetManagment.Service.Interfaces;

namespace BudgetManagment.Service.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IUserService userService;

        private readonly IGenericRepository<Income> repository;
        private readonly IMapper mapper;
        public IncomeService(IUserService userService, IGenericRepository<Income> repository, IMapper mapper)
        {
            this.userService = userService;
            this.repository = repository;
            this.mapper = mapper;
        }

       
        public async Task<IncomeForResultDto> AddAsync(IncomeCreationDto dto)
        {
            var user = await this.userService.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new CustomException(404, "Couldn't find income for given id");

            var income = await this.repository.SelectAsync(i => i.UserId == dto.UserId);
            if (income is not null)
            {
                var updated = await this.repository.UpdateAsync(income);
                updated.UpdatedAt = DateTime.UtcNow;
                await this.repository.SaveAsync();
            }

            var mapped = this.mapper.Map<Income>(dto);
            var inserted = await this.repository.InsertAsync(mapped);
            await this.repository.SaveAsync();

            return this.mapper.Map<IncomeForResultDto>(inserted);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var income = await this.repository.SelectAsync(i => i.Id == id);
            if (income is null)
                throw new CustomException(404, "Couldn't find income for given id");

            var res = await this.repository.DeleteAsync(income);
            await this.repository.SaveAsync();

            return res;
        }

        public async Task<IEnumerable<IncomeForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var incomes = this.repository.SelectAll().ToPagedList(@params);
            if (incomes is null)
                throw new CustomException(404, "Couldn't find incomes for given id");

            return this.mapper.Map<IEnumerable<IncomeForResultDto>>(incomes);
        }

        public async Task<IncomeForResultDto> GetByIdAsync(int id)
        {
            var income = await this.repository.SelectAsync(i => i.Id == id);
            if (income is null)
                throw new CustomException(404, "Couldn't find income for given id");

            return this.mapper.Map<IncomeForResultDto>(income);
        }

        public async Task<IncomeForResultDto> UpdateAsync(int id, IncomeCreationDto dto)
        {
            var income = await this.repository.SelectAsync(i => i.Id == id);
            if (income == null)
                throw new CustomException(404, "Couldn't find user for given id");

            var modified = this.mapper.Map(dto, income);

            var updated = await this.repository.UpdateAsync(modified);
            updated.UpdatedAt = DateTime.UtcNow;
            await this.repository.SaveAsync();

            return this.mapper.Map<IncomeForResultDto>(updated);
        }
    }
}
