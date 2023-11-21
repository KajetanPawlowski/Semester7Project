using Domain.Model;

namespace Application.DAOInterface;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByMailAsync(string mail);
    Task<List<User>> GetAllAsync();
}