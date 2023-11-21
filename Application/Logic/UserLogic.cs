using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    public Task<User> RegisterUserAsync(UserLoginDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<User> ValidateUserAsync(UserLoginDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUserMailAsync(string userMail)
    {
        throw new NotImplementedException();
    }
}
