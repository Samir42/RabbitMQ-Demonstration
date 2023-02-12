using UserService.API.Entities;
using UserService.API.Infrastructure;
using UserService.API.Models.Events;
using UserService.API.Repositories.Abstraction;
using UserService.API.Services.Abstraction;
using Newtonsoft.Json;

namespace UserService.API.Services.Implementation;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRabbitMQService _rabbitMQService;

    public UserService(IUserRepository userRepository, 
        IRabbitMQService rabbitMQService)
    {
        _userRepository = userRepository;
        _rabbitMQService = rabbitMQService;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var createUserResult = await _userRepository.CreateUserAsync(user);

        var userEvent = new UserEvent
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Password = user.Password,
            EventType = "UserCreated"   
        };

        _rabbitMQService.PublishUserCreatedMessage(JsonConvert.SerializeObject(userEvent));

        _rabbitMQService.PublishNotificationMessage();

        return createUserResult;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
}