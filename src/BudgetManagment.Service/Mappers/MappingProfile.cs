using AutoMapper;
using BudgetManagment.Domain.Entities;
using BudgetManagment.Service.DTOs.Expenses;
using BudgetManagment.Service.DTOs.Incomes;
using BudgetManagment.Service.DTOs.User;

namespace BudgetManagment.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user 
            CreateMap<UserCreationDto, User>().ReverseMap();
            CreateMap<UserForResultDto, User>().ReverseMap();
            //Exponse
            CreateMap<Expense, ExpenseCreationDto>().ReverseMap();
            CreateMap<Expense, ExpenseForResultDto>().ReverseMap();
            //Income
            CreateMap<Income, IncomeCreationDto>().ReverseMap();
            CreateMap<Income, IncomeForResultDtod>().ReverseMap();
        }
    }
}
