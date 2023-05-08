using BudgetManagment.DataAccess.Context;
using BudgetManagment.DataAccess.Interfaces;
using BudgetManagment.DataAccess.Repositories;
using BudgetManagment.Service.Interfaces;
using BudgetManagment.Service.Mappers;
using BudgetManagment.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<BmDbContext>(options 
        => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
