using Application.Logic;
using Domain.DTO;
using Domain.Model;

namespace Application.LogicInterface;

public interface IUserLogic
{
    Task<User> RegisterUserAsync(RegisterUserDTO dto);
    Task<User> ValidateUserAsync(UserLoginDTO dto);
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByUserMailAsync(string userMail);
    Task<List<User>> GetUsersAsync();
    Task<User> NotifySupplier(NotifySupplierDTO dto);
}
