using AutoMapper;
using BudgetManagment.DataAccess.Interfaces;
using BudgetManagment.Domain.Configurations;
using BudgetManagment.Domain.Entities;
using BudgetManagment.Service.Commons.Exceptions;
using BudgetManagment.Service.Commons.Extensions;
using BudgetManagment.Service.DTOs.User;
using BudgetManagment.Service.Interfaces;
namespace BudgetManagment.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> repository;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserForResultDto> AddAsync(UserCreationDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (user is not null)
                throw new CustomException(409, "User Already exist");

            var mapped = this.mapper.Map<User>(dto);
            mapped.CreatedAt = DateTime.UtcNow;

            var result = await this.repository.InsertAsync(mapped);
            await this.repository.SaveAsync();

            return this.mapper.Map<UserForResultDto>(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await this.repository.SelectAsync(u => u.Id == id);
            if (user is null)
                throw new CustomException(404, "Couldn't find user for given id");


            var res = await this.repository.DeleteAsync(user);
            await this.repository.SaveAsync();
            return res;
        }

        public async Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var users = this.repository.SelectAll().ToPagedList(@params).ToList();
            if (users.Count == 0)
                throw new CustomException(404, "Couldn't find users");

            return this.mapper.Map<IEnumerable<UserForResultDto>>(users);
        }

        public async Task<UserForResultDto> GetByIdAsync(int id)
        {
            var user = await this.repository.SelectAsync(u => u.Id == id);
            if (user is null)
                throw new CustomException(404, "Couldn't find user for given id");

            return this.mapper.Map<UserForResultDto>(user);

        }

        public async Task<UserForResultDto> UpdateAsync(int id, UserCreationDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.Id == id);
            if (user is null)
                throw new CustomException(404, "Couldn't find user for given id");

            var modified = this.mapper.Map(dto, user);
            await this.repository.SaveAsync();
            modified.UpdatedAt = DateTime.UtcNow;

            return this.mapper.Map<UserForResultDto>(modified);
        }
    }
}
