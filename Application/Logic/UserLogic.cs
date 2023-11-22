using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO _userDao;
    public UserLogic(IUserDAO userDao)
    {
        _userDao = userDao;
    }
    public async Task<User> RegisterUserAsync(UserLoginDTO dto)
    {
        User? existing = await _userDao.GetByMailAsync(dto.UserMail);
        if (existing != null)
            throw new Exception("UserMail already in used!");
        
        ValidateRegistrationData(dto);
        User toCreate = new User 
        {
            Mail = dto.UserMail,
            Password = dto.Password,
            Role = "woltSupplier"
            
        };
    
        User created = await _userDao.CreateAsync(toCreate);
    
        return created;
    }

    public async Task<User> ValidateUserAsync(UserLoginDTO dto)
    {
        User? existingUser = await _userDao.GetByMailAsync(dto.UserMail);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(dto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return _userDao.GetByIdAsync(userId);
    }

    public Task<User?> GetByUserMailAsync(string userMail)
    {
        return _userDao.GetByMailAsync(userMail);
    }

    public Task<List<User>> GetUsersAsync()
    {
        return _userDao.GetAllAsync();
    }

    private static void ValidateRegistrationData(UserLoginDTO userToCreate)
    {
        string userMail = userToCreate.UserMail;
        string password = userToCreate.Password;

        if (userMail.Length < 3)
            throw new Exception("User mail must be at least 3 characters!");
        
        if (password.Length < 3)
            throw new Exception("Password must be at least 3 characters!");

        if (password.Length > 15)
            throw new Exception("Password must be less than 16 characters!");
    }
}
