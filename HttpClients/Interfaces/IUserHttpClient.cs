using Domain.Model;

namespace HttpClients.Interfaces;

public interface IUserHttpClient
{
    public Task<List<string>> GetUsersMailsAsync();
    public Task<List<User>> GetUsersAsync();
}