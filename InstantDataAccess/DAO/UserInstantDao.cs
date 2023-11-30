using Application.DAOInterface;
using Domain.Model;

namespace InstantDataAccess.DAO;

public class UserInstantDao : IUserDAO
{
    private readonly InstantDataContext context;
    
    public UserInstantDao(InstantDataContext context)
    {
        this.context = context;
    }
    
    public Task<User> CreateAsync(User user)
    {
        int nextId = 1;
        if (context.Users.Any())
        {
            nextId = context.Users.Max(s => s.Id);
            nextId++;
        }

        user.Id = nextId;
        
        context.Users.Add(user);
        context.SaveChanges();
        
        return Task.FromResult(user);
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Id == userId);
        if(existing == null)
        {
            throw new Exception("User not found");
        }

        return await Task.FromResult(existing);
    }

    public Task<User> GetByMailAsync(string mail)
    {
        // Assuming you have a List<User> users defined somewhere
        User user = context.Users.FirstOrDefault(u => u.Mail.Equals(mail));
        
        return Task.FromResult(user);
    }

    public Task<List<User>> GetAllAsync()
    {
        return Task.FromResult(context.Users.ToList());
    }
}