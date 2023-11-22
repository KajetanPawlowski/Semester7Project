using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class UserInstantDao : IUserDAO
{
    private readonly InstantDataContext context;
    
    public UserInstantDao(InstantDataContext context)
    {
        this.context = this.context;
    }
    
    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByMailAsync(string mail)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}