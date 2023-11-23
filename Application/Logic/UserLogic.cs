using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO _userDao;
    private const string DEFAULT_PASSWORD = "pass";
    public UserLogic(IUserDAO userDao)
    {
        _userDao = userDao;
    }
    public async Task<User> RegisterUserAsync(RegisterUserDTO dto)
    {
        User? existing = await _userDao.GetByMailAsync(dto.UserMail);
        if (existing != null)
            throw new Exception("UserMail already in used!");
        
        ValidateRegistrationData(dto);
        User toCreate = new User 
        {
            Mail = dto.UserMail,
            Password = DEFAULT_PASSWORD,
            FullName = dto.FullName,
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

    private static void ValidateRegistrationData(RegisterUserDTO userToCreate)
    {
        string userMail = userToCreate.UserMail;

        if (userMail.Length < 3)
            throw new Exception("User mail must be at least 3 characters!");
        
    }
}
