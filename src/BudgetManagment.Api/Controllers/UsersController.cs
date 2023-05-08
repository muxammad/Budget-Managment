using BudgetManagment.Domain.Configurations;
using BudgetManagment.Service.DTOs.User;
using BudgetManagment.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagment.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        private short _pageSize = 20;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostUserAsync(UserCreationDto dto)
        {
            return Ok(new
            {
                Code = 200,
                Error = "Succes",
                Data = await this.userService.AddAsync(dto)
            });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(int id) =>
            Ok(new
            {
                Code = 200,
                Error = "Succes",
                Data = await this.userService.DeleteAsync(id)
            });
        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(int id)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.userService.GetByIdAsync(id)
         });

        [HttpGet("Get-Users-list")]
        public async Task<IActionResult> GetAllUserAsync([FromQuery] PaginationParams @params) =>
            Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.userService.GetAllAsync(@params)
            });

        [HttpPut("Update-User")]
        public async Task<IActionResult> PutUserAsync(int id,UserCreationDto dto) =>
            Ok(new
            {
                Code = 200,
                Erroe = "Succes",
                Data =  await this.userService.UpdateAsync(id,dto)
            });

    }
}
