using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface IUserLogic
{
    Task<User> RegisterUserAsync(string userMail, string password);
    Task<User> ValidateUserAsync(string userMail, string password);
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByUserMailAsync(string userMail);
}
