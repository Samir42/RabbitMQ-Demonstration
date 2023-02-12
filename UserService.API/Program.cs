using UserService.API.Persistence;
using Microsoft.EntityFrameworkCore;
using UserService.API.Repositories.Implementation;
using UserService.API.Repositories.Abstraction;
using UserService.API.Services.Abstraction;
using Implementation = UserService.API.Services.Implementation;
using UserService.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseInMemoryDatabase("UserDatabase"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, Implementation.UserService>();
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();


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
